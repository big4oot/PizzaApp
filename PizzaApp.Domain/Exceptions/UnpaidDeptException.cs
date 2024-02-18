namespace PizzaApp.Domain.Exceptions
{
    public class UnpaidDeptException : Exception
    {
        public UnpaidDeptException() : base("Невозможно сделать заказ. Есть неоплаченные заказы."){ }
    }
}
