using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;

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

        public async Task<LoginInfoModel> AuthenticateAsync(string username, string password)
        {
            return (await _authService.SignInAsync(username, password))!;
        }

        public EnUserValidateStatus ValidateUserAccount(LoginInfoModel? loginInfo)
        {
            if (loginInfo == null)
            {
                return EnUserValidateStatus.NotFound;
            }

            if (!loginInfo.UserLogin.IsActive)
            {
                return EnUserValidateStatus.NotActive;
            }

            return EnUserValidateStatus.FoundAndActive;
        }
    }
}
