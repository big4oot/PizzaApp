using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PizzaApp.Application.Common.Interfaces;

namespace PizzaApp.Application.CQs.Accounts.Commands.LoginAccount
{
    public class LoginAccountCommand : IRequest<SignInResult?>
    {
        [Display(Name = "Имя пользователя")]
        public string? UserName { get; set; }

        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }

    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, SignInResult?>
    {
        private readonly IIdentityService _identityService;

        public LoginAccountCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<SignInResult?> Handle(LoginAccountCommand request, CancellationToken cancellationToken){
            if (request.UserName == null || request.Password == null) { return null ; }
            return await _identityService.AuthorizeAsync(request.UserName, request.Password);
        }
    }
}
