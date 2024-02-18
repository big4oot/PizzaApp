namespace PizzaApp.Domain.Exceptions
{
    public class OrderAlreadyPaidException : Exception
    {
        public OrderAlreadyPaidException() : base("Заказ уже оплачен.")
        {
            
        }
    }
}
