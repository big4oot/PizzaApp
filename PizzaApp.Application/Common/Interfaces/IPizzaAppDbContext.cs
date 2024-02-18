using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Entities;

namespace PizzaApp.Application.Common.Interfaces
{
    public interface IPizzaAppDbContext
    {
        public DbSet<Order> Orders { get; }
        public DbSet<Pizza> Pizzas { get; }
        public DbSet<Wallet> Wallets { get; }
        public DbSet<Invoice> Invoices { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
