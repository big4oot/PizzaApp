using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.Exceptions;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Exceptions;

namespace PizzaApp.Application.CQs.Orders.Commands.PayForOrder
{
    public class PayForOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }

    public class PayForOderCommandHandler : IRequestHandler<PayForOrderCommand>
    {
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IPaymentService _paymentService;
        private readonly IUser _user;

        public PayForOderCommandHandler(IPizzaAppDbContext dbContext, IPaymentService paymentService, IUser user)
        {
            _dbContext = dbContext;
            _paymentService = paymentService;
            _user = user;
        }

        public async Task Handle(PayForOrderCommand request, CancellationToken cancellationToken)
        {
            #region Get order

            var order = _dbContext.Orders.Include(o => o.Invoice).FirstOrDefault(o => o.Id == request.OrderId);
            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            if (order.Invoice.Status == InvoiceStatus.Paid)
            {
                throw new OrderAlreadyPaidException();
            }

            #endregion

            await _paymentService.PayForOrderAsync(order, _user.Id);
        }
    }
}
