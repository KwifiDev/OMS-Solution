using CommunityToolkit.Mvvm.ComponentModel;
using OMS.UI.Models;

namespace OMS.UI.Services.UserSession
{
    public partial class UserSessionService : ObservableObject, IUserSessionService
    {

        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private UserLoginModel? _currentUser;


        public void Login(UserLoginModel user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
        }

        public void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }

    }
}
