using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Domain.Constants;
using PizzaApp.Domain.Entities;
using PizzaApp.Infrastructure.Identity;

namespace PizzaApp.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static void Initialize(WebApplication application)
        {
            using var serviceScope = application.Services.CreateScope();
            Seed(serviceScope.ServiceProvider.GetRequiredService<PizzaAppDbContext>(), 
                serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(), 
                serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>());
        }

        private static void Seed(PizzaAppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            #region Seed pizzas

            var pizza1 = new Pizza() { Name = "BBQ", Cost = 100 };
            var pizza2 = new Pizza() { Name = "Veggie", Cost = 120 };
            var pizza3 = new Pizza() { Name = "Cheese", Cost = 140 };
            var pizza4 = new Pizza() { Name = "Meat", Cost = 160 };

            dbContext.Pizzas.AddRange(new List<Pizza>() { pizza1, pizza2, pizza3, pizza4 });
        
            #endregion

            #region Seed roles

            var adminRole = new IdentityRole(Roles.Admin);
            roleManager.CreateAsync(adminRole);

            var clientRole = new IdentityRole(Roles.Client);
            roleManager.CreateAsync(clientRole);

            #endregion

            #region Seed users

            var admin = new ApplicationUser { UserName = "admin" };
            userManager.CreateAsync(admin, "admin");
            userManager.AddToRolesAsync(admin, new[] { adminRole.Name });
            var walletAdm = new Wallet()
            {
                Balance = 350,
                CreatedBy = admin.Id
            };

            var client = new ApplicationUser { UserName = "tom" };

            userManager.CreateAsync(client, "123");
            userManager.AddToRolesAsync(client, new[] { clientRole.Name });

            var wallet = new Wallet()
            {
                Balance = 350,
                CreatedBy = client.Id
            };

            dbContext.Wallets.AddRange(walletAdm, wallet);

            dbContext.SaveChanges();

            #endregion
        }
    }
}
