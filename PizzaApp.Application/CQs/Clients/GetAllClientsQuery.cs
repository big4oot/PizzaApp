using MediatR;
using PizzaApp.Application.Common.DTOs;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Clients
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IPizzaAppDbContext _context;
        private readonly IIdentityService _identityService;

        public GetAllClientsQueryHandler(IPizzaAppDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _identityService.GetUsersInRoleAsync("Client");

            clients = clients.Select(clientDto =>
            {
                clientDto.OrdersCount = _context.Orders.Count(c => c.CreatedBy == clientDto.Id);
                clientDto.Balance = _context.Wallets.FirstOrDefault(w => w.CreatedBy == clientDto.Id)?.Balance;
                clientDto.HasDebt = _context.Invoices.Any(i => i.IsOverdue && i.CreatedBy == clientDto.Id);
                return clientDto;
            });

            return clients;
        }
    }
}
