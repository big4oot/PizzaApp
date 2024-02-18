using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.DTOs;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Pizzas.Queries
{
    public class GetAvailablePizzasQuery : IRequest<IEnumerable<PizzaDto>>
    {
    }

    public class GetAvailablePizzasQueryHandler : IRequestHandler<GetAvailablePizzasQuery, IEnumerable<PizzaDto>>
    {
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAvailablePizzasQueryHandler(IPizzaAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PizzaDto>> Handle(GetAvailablePizzasQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Pizzas
                .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
