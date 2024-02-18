using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Entities;

namespace PizzaApp.Application.CQs.Accounts.Commands.RegisterAccount
{
    public class RegisterAccountCommand : IRequest<IdentityResult?>
    {
        [Display(Name = "Имя пользователя")]
        public string? UserName { get; set; }

        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }

    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, IdentityResult?>
    {
        private readonly IIdentityService _identityService;
        private readonly IPizzaAppDbContext _dbContext;

        public RegisterAccountCommandHandler(IIdentityService identityService, IPizzaAppDbContext dbContext)
        {
            _identityService = identityService;
            _dbContext = dbContext;
        }

        public async Task<IdentityResult?> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)

        {
            if (request.UserName == null || request.Password == null) { return null; }

            var (result, userId) =  await _identityService.CreateUserAsync(request.UserName, request.Password);

            var wallet = new Wallet()
            {
                Balance = 350,
                CreatedBy = userId
            };

            _dbContext.Wallets.Add(wallet);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
