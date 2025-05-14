using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        //private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
            //_mapper = mapper;
        }

        public async Task<UserLoginModel> AuthenticateAsync(string username, string password)
        {
            return (await _userService.GetByUsernameAndPasswordAsync(username, password))!;
        }
    }
}
