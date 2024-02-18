using Microsoft.AspNetCore.Identity;
using PizzaApp.Application.Common.DTOs;

namespace PizzaApp.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> IsInRoleAsync(string userId, string role);

        Task<SignInResult?> AuthorizeAsync(string userName, string password);

        Task<(IdentityResult? result, string Id)> CreateUserAsync(string userName, string password);

        Task LogoutAsync();

        Task<IEnumerable<ClientDto>> GetUsersInRoleAsync(string role);
    }
}
