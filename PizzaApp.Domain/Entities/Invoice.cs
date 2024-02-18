using PizzaApp.Domain.Common;
using PizzaApp.Domain.Enums;

namespace PizzaApp.Domain.Entities
{

    /// <summary>
    /// Счет на оплату
    /// </summary>
    public class Invoice : BaseAuditableEntity
    {

        /// <summary>
        /// Id (PK)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Сумма счета
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Дата оплаты счета
        /// </summary>
        public DateTimeOffset? PaymentDatetime { get; set; }

        /// <summary>
        /// Статус счета
        /// </summary>
        public InvoiceStatus Status { get; set; } = InvoiceStatus.PaymentAwaiting;

        /// <summary>
        /// Счет просрочен
        /// </summary>
        public bool IsOverdue
        {
            get
            {
                var currentDatetime = DateTimeOffset.Now;
                var dt = currentDatetime - Created;
                return dt.Days > 7;
            }
            set
            {

            }
    }
    }
}
