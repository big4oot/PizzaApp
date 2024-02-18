namespace PizzaApp.Application.Common.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string exMessage) : base(exMessage) {}
}