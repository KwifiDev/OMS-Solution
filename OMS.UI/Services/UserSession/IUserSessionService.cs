using OMS.UI.Models;

namespace OMS.UI.Services.UserSession
{
    public interface IUserSessionService
    {
        UserModel? CurrentUser { get; }
        bool IsLoggedIn { get; }
        void Login(UserModel user);
        void Logout();
    }
}
