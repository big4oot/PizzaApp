using PizzaApp.Domain.Entities;

namespace PizzaApp.Application.Common.Interfaces
{
    public interface IPaymentService
    {
        public Task PayForOrderAsync(Order order, string userId);
        public Task PayForAllOrdersAsync(string userId);
    }
}
