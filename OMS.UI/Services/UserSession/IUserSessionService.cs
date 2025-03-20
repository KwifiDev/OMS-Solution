using OMS.UI.Models;

namespace OMS.UI.Services.UserSession
{
    public interface IUserSessionService
    {
        UserLoginModel? CurrentUser { get; }
        bool IsLoggedIn { get; }
        void Login(UserLoginModel user);
        void Logout();
        Task UpdateModel();
    }
}
