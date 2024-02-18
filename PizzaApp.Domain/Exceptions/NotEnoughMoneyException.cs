namespace PizzaApp.Domain.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException() : base("Недостаточно средств для оплаты.") { }
    }
}
