namespace PizzaApp.Application.Common.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("Заказ не найден") { }
    }
}
