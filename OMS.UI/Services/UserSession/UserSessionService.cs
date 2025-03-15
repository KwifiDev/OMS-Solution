using CommunityToolkit.Mvvm.ComponentModel;
using OMS.UI.Models;

namespace OMS.UI.Services.UserSession
{
    public partial class UserSessionService : ObservableObject, IUserSessionService
    {

        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private UserModel? _currentUser;


        public void Login(UserModel user)
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
