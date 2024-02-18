using Microsoft.AspNetCore.Identity;

namespace PizzaApp.Infrastructure.Identity.Errors
{

    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Минимальная длина пароля {length} символа(ов)." }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Имя пользователя '{userName}' уже занято." }; }

    }
}
