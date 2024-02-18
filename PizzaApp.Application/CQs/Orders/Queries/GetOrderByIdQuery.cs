using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.DTOs;
using PizzaApp.Application.Common.Exceptions;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IPizzaAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Pizza)
                .Include(o => o.Invoice)
                .FirstOrDefaultAsync(o => o.Id == request.Id);

            if (order == null)
            {
                throw new ItemNotFoundException("Заказ не найден");  
            }

            return _mapper.Map<OrderDto>(order);
        }
    }
}
