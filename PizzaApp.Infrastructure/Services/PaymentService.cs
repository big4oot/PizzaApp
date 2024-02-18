using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Entities;
using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Exceptions;

namespace PizzaApp.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPizzaAppDbContext _dbContext;

        public PaymentService(IPizzaAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task PayForOrderAsync(Order order, string userId)
        {
            var wallet = GetUserWallet(userId);

            if (wallet.Balance < order.Invoice.Amount)
            {
                throw new NotEnoughMoneyException();
            }

            order.Invoice.Status = InvoiceStatus.Paid;
            order.Invoice.PaymentDatetime = DateTime.Now;
            wallet.Charge(order.Invoice.Amount);

            _dbContext.Orders.Update(order);
            _dbContext.Wallets.Update(wallet);
            _dbContext.Invoices.Update(order.Invoice);

            await _dbContext.SaveChangesAsync(new CancellationToken());
        }

        public async Task PayForAllOrdersAsync(string userId)
        {
            var orders = _dbContext.Orders.Where(o => o.CreatedBy == userId).Include(o => o.Invoice);
            var totalAmount = Enumerable.Sum(orders, order => order.Invoice.Amount);

            var wallet = GetUserWallet(userId);

            if (wallet.Balance < totalAmount)
            {
                throw new NotEnoughMoneyException();
            }

            foreach (var order in orders)
            {
                order.Invoice.Status = InvoiceStatus.Paid;
                order.Invoice.PaymentDatetime = DateTime.Now;
                wallet.Charge(order.Invoice.Amount);

                _dbContext.Orders.Update(order);
                _dbContext.Invoices.Update(order.Invoice);
            }

            _dbContext.Wallets.Update(wallet);


            await _dbContext.SaveChangesAsync(new CancellationToken());
        }

        private Wallet GetUserWallet(string userId)
        {
            return _dbContext.Wallets.FirstOrDefault(w => w.CreatedBy == userId);
        }
    }
}
