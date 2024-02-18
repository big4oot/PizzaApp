using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.Exceptions;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Application.CQs.Orders.Commands.CreateOrder;
using PizzaApp.Domain.Entities;
using PizzaApp.Domain.Exceptions;

namespace PizzaApp.Application.CQs.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        [Display(Name = "Вид пиццы")]
        public Pizza Pizza { get; set; }
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IPizzaAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public CreateOrderCommandHandler(IPizzaAppDbContext dbContext, IMapper mapper, IUser user)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _user = user;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = _user.Id;

        #region Get pizza

        var pizza = _dbContext.Pizzas.FirstOrDefault(p => p.Name == request.Pizza.Name);
        if (pizza == null)
        {
            throw new ItemNotFoundException("Пицца не найдена.");
        }

        #endregion

        #region Check that user can order

        var ordersWithOverdueInvoices = _dbContext.Orders.Include(o => o.Invoice).Where(o => o.CreatedBy == user && o.Invoice.IsOverdue);
        if (ordersWithOverdueInvoices.Any())
        {
            throw new UnpaidDeptException();
        }

        #endregion

        #region Create order

        var order = new Order()
        {
            Pizza = pizza
        };

        var invoice = new Invoice()
        {
            Order = order
        };

        order.Invoice = invoice;

        order.CalculateAmount();

        await _dbContext.Orders.AddAsync(order, cancellationToken);

        await _dbContext.Invoices.AddAsync(invoice);

        await _dbContext.SaveChangesAsync(cancellationToken);

        #endregion


        return order.Id;
        
    }
}
