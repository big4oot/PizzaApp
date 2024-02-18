using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.DTOs;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Constants;

namespace PizzaApp.Application.CQs.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        private readonly IIdentityService _identityService;

        public GetAllOrdersQueryHandler(IPizzaAppDbContext dbContext, IMapper mapper, IUser user, IIdentityService identityService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _user = user;
            _identityService = identityService;
        }

        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var isAdmin = await _identityService.IsInRoleAsync(_user.Id, Roles.Admin);

            if (isAdmin)
            {
                return await _dbContext.Orders
                    .Include(o => o.Invoice)
                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            return await _dbContext.Orders.Where(o => o.CreatedBy == _user.Id)
                .Include(o => o.Invoice)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
