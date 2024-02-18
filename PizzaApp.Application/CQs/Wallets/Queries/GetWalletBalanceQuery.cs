using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Wallets.Queries
{
    public class GetWalletBalanceQuery : IRequest<int?>
    {
    }

    public class GetWalletBalanceQueryHandler : IRequestHandler<GetWalletBalanceQuery, int?>
    {
        private readonly IUser _user;
        private readonly IPizzaAppDbContext _context;
        public GetWalletBalanceQueryHandler(IUser user, IPizzaAppDbContext context)
        {
            _user = user;
            _context = context;
        }

        public async Task<int?> Handle(GetWalletBalanceQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.CreatedBy == _user.Id);
            return wallet?.Balance;
        }
    }
}
