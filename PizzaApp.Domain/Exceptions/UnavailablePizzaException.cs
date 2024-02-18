namespace PizzaApp.Domain.Exceptions
{
    public class UnavailablePizzaException : Exception
    {
        public UnavailablePizzaException(string pizzaName) : base($"Pizza {pizzaName} is unavailable")
        {
        }
    }
}
