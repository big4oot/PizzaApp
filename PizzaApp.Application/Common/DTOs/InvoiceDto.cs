using PizzaApp.Domain.Entities;
using AutoMapper;

namespace PizzaApp.Application.Common.DTOs
{
    /// <summary>
    /// DTO счета на оплату
    /// </summary>
    public class InvoiceDto
    {

        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Id клиента
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Дата оплаты счета
        /// </summary>
        public DateTimeOffset? PaymentDatetime { get; set; }

        /// <summary>
        /// Сумма счета
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Счет просрочен
        /// </summary>
        public bool IsOverdue { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Invoice, InvoiceDto>()
                    .ForMember(dest => dest.OrderId, src => src.MapFrom(_ => _.Order.Id))
                    .ForMember(dest => dest.ClientId, src => src.MapFrom(_ => _.CreatedBy))
                    .ForMember(dest => dest.CreatedDate, src => src.MapFrom(_ => _.Created))
                    .ForMember(dest => dest.PaymentDatetime, src => src.MapFrom(_ => _.PaymentDatetime))
                    .ForMember(dest => dest.Amount, src => src.MapFrom(_ => _.Amount))
                    .ForMember(dest => dest.IsOverdue, src => src.MapFrom(_ => _.IsOverdue));
            }
        }
    }
}