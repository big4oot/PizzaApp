using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Infrastructure.Identity;
using PizzaApp.Infrastructure.Identity.Errors;
using PizzaApp.Infrastructure.Persistence;
using PizzaApp.Infrastructure.Persistence.Interceptors;
using PizzaApp.Infrastructure.Services;

namespace PizzaApp.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(); 

            services.AddDbContext<PizzaAppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseInMemoryDatabase("PizzaAppInMemoryDb");
            });

            services.AddScoped<IPizzaAppDbContext>(provider => provider.GetRequiredService<PizzaAppDbContext>());
            services.AddScoped<IPaymentService, PaymentService>();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 3;

                    opt.User.RequireUniqueEmail = false;

                    opt.SignIn.RequireConfirmedAccount = false;
                    opt.SignIn.RequireConfirmedEmail = false;

                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PizzaAppDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddTransient<IIdentityService, IdentityService>();


            return services;
        }
    }
}
