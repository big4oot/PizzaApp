using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Entities;
using PizzaApp.Infrastructure.Identity;

namespace PizzaApp.Infrastructure.Persistence
{
    public class PizzaAppDbContext : IdentityDbContext<ApplicationUser>, IPizzaAppDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public PizzaAppDbContext(DbContextOptions<PizzaAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Invoice>().Property(i => i.IsOverdue).UsePropertyAccessMode(PropertyAccessMode.Property);
            base.OnModelCreating(builder);
        }
    }
}
