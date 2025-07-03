using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public enum EnUserValidateStatus { NotFound, NotActive, FoundAndActive }

        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserLoginModel> AuthenticateAsync(string username, string password)
        {
            return (await _userService.GetByUsernameAndPasswordAsync(username, password))!;
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
