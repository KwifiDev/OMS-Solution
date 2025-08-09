using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Services.Registry;

namespace OMS.UI.Services.UserSession
{
    public partial class UserSessionService : ObservableObject, IUserSessionService
    {
        private readonly IUserService _userService;
        private readonly IRegistryService _registryService;

        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private UserLoginModel? _currentUser;


        public UserSessionService(IUserService userService, IRegistryService registryService)
        {
            _userService = userService;
            _registryService = registryService;
        }

        public void Login(UserLoginModel user, string hashPassword, bool isRememberMe = false)
        {
            CurrentUser = user;

            if (isRememberMe)
                _registryService.SetUserLoginConfig(user.Username, hashPassword);
            else
                _registryService.ResetUserLoginConfig();

            IsLoggedIn = true;
        }

        public void Logout()
        {
            CurrentUser = null;
            _registryService.ResetUserLoginConfig();

            IsLoggedIn = false;
        }

        public async Task UpdateModel()
        {
            var userLoginModel = await _userService.GetUserLoginByPersonIdAsync(CurrentUser!.PersonId);

            CurrentUser = userLoginModel ?? CurrentUser;

            WeakReferenceMessenger.Default.Send(CurrentUser);
        }
    }
}
