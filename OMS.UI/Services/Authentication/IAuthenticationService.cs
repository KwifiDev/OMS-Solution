using OMS.UI.Models.Others;
using static OMS.UI.Services.Authentication.AuthenticationService;

namespace OMS.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginInfoModel> AuthenticateAsync(string username, string password);
        EnUserValidateStatus ValidateUserAccount(LoginInfoModel? loginInfo);
    }
}
