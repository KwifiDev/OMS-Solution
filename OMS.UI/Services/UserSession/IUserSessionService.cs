using OMS.UI.Models.Others;

namespace OMS.UI.Services.UserSession
{
    public interface IUserSessionService
    {
        UserLoginModel? CurrentUser { get; }
        TokenModel? CurrentToken { get; }
        IEnumerable<string>? Claims { get; }
        bool IsLoggedIn { get; }
        void Login(LoginInfoModel loginInfo, string password, bool isRememberMe = false);
        void Logout();
        Task UpdateModel();
        Task UpdateToken();
        Task UpdateClaims();
        IEnumerable<string> GetUserRoles();
    }
}
