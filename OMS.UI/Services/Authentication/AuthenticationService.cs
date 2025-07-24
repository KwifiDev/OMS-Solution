using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public enum EnUserValidateStatus { NotFound, NotActive, FoundAndActive }

        private readonly IAuthService _authService;

        public AuthenticationService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<UserLoginModel> AuthenticateAsync(string username, string password)
        {
            return (await _authService.SignInAsync(username, password))!;
        }

        public EnUserValidateStatus ValidateUserAccount(UserLoginModel? user)
        {
            if (user == null)
            {
                return EnUserValidateStatus.NotFound;
            }

            if (!user.IsActive)
            {
                return EnUserValidateStatus.NotActive;
            }

            return EnUserValidateStatus.FoundAndActive;
        }
    }
}
