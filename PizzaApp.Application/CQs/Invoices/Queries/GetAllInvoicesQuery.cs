using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.DTOs;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Invoices.Queries
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
    {
    }

    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IPizzaAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllInvoicesQueryHandler(IPizzaAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Invoices.Include(i => i.Order)
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
