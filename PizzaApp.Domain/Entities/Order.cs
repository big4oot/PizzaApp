using PizzaApp.Domain.Common;

namespace PizzaApp.Domain.Entities;


/// <summary>
/// Заказ
/// </summary>
public class Order : BaseAuditableEntity
{

    /// <summary>
    /// Id (PK)
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id пиццы
    /// </summary>
    public int PizzaId { get; set; }

    /// <summary>
    /// Пицца
    /// </summary>
    public Pizza Pizza { get; set; }

    /// <summary>
    /// Счет на оплату
    /// </summary>
    public Invoice Invoice { get; set; }


    /// <summary>
    /// Вычислить сумму счета
    /// </summary>
    public void CalculateAmount()
    {
        Invoice.Amount += Pizza.Cost;
    }
}