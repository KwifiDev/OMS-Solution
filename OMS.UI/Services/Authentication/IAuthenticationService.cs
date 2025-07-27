using OMS.UI.Models.Others;
using static OMS.UI.Services.Authentication.AuthenticationService;

namespace OMS.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserLoginModel> AuthenticateAsync(string username, string password);
        EnUserValidateStatus ValidateUserAccount(UserLoginModel? user);
    }
}
