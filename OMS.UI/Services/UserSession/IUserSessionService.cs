using OMS.UI.Models.Others;

namespace OMS.UI.Services.UserSession
{
    public interface IUserSessionService
    {
        UserLoginModel? CurrentUser { get; }
        bool IsLoggedIn { get; }
        void Login(UserLoginModel user, string hashPassword, bool isRememberMe = false);
        void Logout();
        Task UpdateModel();
    }
}
