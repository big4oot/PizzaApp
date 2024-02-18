using PizzaApp.Domain.Entities;
using PizzaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace PizzaApp.Application.Common.DTOs
{
    /// <summary>
    /// DTO заказа
    /// </summary>
    public class OrderDto
    {
        [Display(Name = "Номер заказа")]
        public int Id { get; set; }

        [Display(Name = "Клиент")]
        public string CreatedBy{ get; set; }

        [Display(Name = "Дата заказа")]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Статус заказа")]
        public InvoiceStatus Status { get; set; }

        [Display(Name = "Стоимость")]
        public int Amount { get; set; }

        [Display(Name = "Содержимое заказа")]
        public Pizza Pizza { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.Amount, src => src.MapFrom(_ => _.Invoice.Amount))
                    .ForMember(dest => dest.Status, src => src.MapFrom(_ => _.Invoice.Status))
                    .ForMember(dest => dest.Created, src => src.MapFrom(_ => _.Invoice.Created));
            }
        }
    }
}
