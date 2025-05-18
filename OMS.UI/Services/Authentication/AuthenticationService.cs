using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserLoginModel> AuthenticateAsync(string username, string password)
        {
            return (await _userService.GetByUsernameAndPasswordAsync(username, password))!;
        }
    }
}
