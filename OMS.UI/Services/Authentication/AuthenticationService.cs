using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserLoginModel> AuthenticateAsync(string username, string password)
        {
            var userLoginDto = await _userService.GetByUsernameAndPasswordAsync(username, password);
            return _mapper.Map<UserLoginModel>(userLoginDto);
        }
    }
}
