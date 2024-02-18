using PizzaApp.Application;
using PizzaApp.Infrastructure;
using PizzaApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Constants;
using PizzaApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddCoreAdmin(nameof(Roles.Admin));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddScoped<IUser, CurrentUser>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();


DatabaseInitializer.Initialize(app);  // Move to app config layer

app.Run();
