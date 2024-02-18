using MediatR;
using PizzaApp.Application.Common.Exceptions;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Orders.Commands.PayForAllOrders
{
    public class PayForAllOrdersCommand : IRequest
    {
    }

    public class PayForAllOrdersCommandHandler : IRequestHandler<PayForAllOrdersCommand>
    {
        private readonly IUser _user;
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IPaymentService _paymentService;

        public PayForAllOrdersCommandHandler(IUser user, IPizzaAppDbContext dbContext, IPaymentService paymentService)
        {
            _user = user;
            _dbContext = dbContext;
            _paymentService = paymentService;
        }

        public async Task Handle(PayForAllOrdersCommand request, CancellationToken cancellationToken)
        {
            #region Get orders

            var orders = _dbContext.Orders.Where(o => o.CreatedBy == _user.Id);
            if (!orders.Any())
            {
                throw new OrderNotFoundException();
            }

            #endregion

            await _paymentService.PayForAllOrdersAsync(_user.Id);
        }
    }
}
