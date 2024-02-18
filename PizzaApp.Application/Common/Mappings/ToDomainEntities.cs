using AutoMapper;
using PizzaApp.Application.CQs.Orders.Commands.CreateOrder;
using PizzaApp.Domain.Entities;
using PizzaApp.Domain.Enums;

namespace PizzaApp.Application.Common.Mappings
{
    public class ToDomainEntities : Profile
    {
        public ToDomainEntities()
        {
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForPath(dest => dest.Invoice.Status, opt => opt.MapFrom(_ => InvoiceStatus.PaymentAwaiting));
        }
    }
}
