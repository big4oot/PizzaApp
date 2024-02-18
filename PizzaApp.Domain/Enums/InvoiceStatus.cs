using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Domain.Enums
{

    /// <summary>
    /// Статус счета на оплату
    /// </summary>
    public enum InvoiceStatus
    {
        [Display(Name = "Ожидает оплаты")]
        PaymentAwaiting,

        [Display(Name = "Оплачен")]
        Paid,
    }
}
