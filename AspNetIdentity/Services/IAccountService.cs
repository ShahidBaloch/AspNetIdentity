using AspNetIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentity.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
        Task<SignInResult> LoginUserAsync(LoginViewModel model);
        Task LogoutUserAsync();
        Task SendEmailConfirmationAsync(string email);
        Task<ProfileViewModel> GetUserProfileByEmailAsync(string email);
    }
}
