using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Application.CQs.Accounts.Commands.LoginAccount;
using PizzaApp.Application.CQs.Accounts.Commands.RegisterAccount;
using PizzaApp.Application.CQs.Accounts.Logout;

namespace PizzaApp.Web.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Register actions

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterAccountCommand command)
    {
        var result = await _mediator.Send(command);
        if (result != null && result.Succeeded) return RedirectToAction("Index", "Home");
        if (result == null)
        {
            ModelState.AddModelError("", "Проверьте корректность введенных данных.");
            return View();
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View();
    }

    #endregion

    #region Login Actions

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginAccountCommand command)
    {
        var result = await _mediator.Send(command);
        if (result != null && result.Succeeded ) return RedirectToAction("Index", "Home");
        if (result == null)
        {
            ModelState.AddModelError("", "Проверьте корректность введенных данных.");
            return View();
        }
        ModelState.AddModelError("", "Неверное имя пользователя или пароль.");

        return View();
    }

    #endregion

    #region Logout actions

    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutCommand());
        return RedirectToAction("Login", "Account");
    }

    #endregion

    #region Access denied actions

    public IActionResult AccessDenied(string returnUrl)
    {
        return View();
    }

    #endregion
}