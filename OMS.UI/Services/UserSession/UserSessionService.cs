using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Services.JWT;
using OMS.UI.Services.Registry;

namespace OMS.UI.Services.UserSession
{
    public partial class UserSessionService : ObservableObject, IUserSessionService
    {

        private readonly IUserService _userService;
        private readonly IRegistryService _registryService;
        private readonly IAuthService _authService;
        private readonly IJwtPayloadService _jwtPayloadService;

        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private UserLoginModel? _currentUser;

        [ObservableProperty]
        private TokenModel? _currentToken;

        private IEnumerable<string> _claims = [];


        public event Action? ClaimsChanged;


        public UserSessionService(IUserService userService, IRegistryService registryService, IAuthService authService, IJwtPayloadService jwtPayloadService)
        {
            _userService = userService;
            _registryService = registryService;
            _authService = authService;
            _jwtPayloadService = jwtPayloadService;
        }


        public IEnumerable<string> Claims
        {
            get => _claims;
            set
            {
                SetProperty(ref _claims, value);
                OnClaimsChanged();
            }
        }



        public void Login(LoginInfoModel loginInfo, string password, bool isRememberMe = false)
        {
            CurrentUser = loginInfo.UserLogin;
            CurrentToken = loginInfo.TokenInfo;
            Claims = loginInfo.Claims ?? Enumerable.Empty<string>();

            if (isRememberMe)
                _registryService.SetUserLoginConfig(loginInfo.UserLogin.Username, password);
            else
                _registryService.ResetUserLoginConfig();

            IsLoggedIn = true;
        }

        public void Logout()
        {
            CurrentUser = null;
            CurrentToken = null;
            Claims = Enumerable.Empty<string>();

            _registryService.ResetUserLoginConfig();

            IsLoggedIn = false;
        }

        public async Task UpdateModel()
        {
            var userLoginModel = await _userService.GetUserLoginByPersonIdAsync(CurrentUser!.PersonId);

            CurrentUser = userLoginModel ?? CurrentUser;

            WeakReferenceMessenger.Default.Send(CurrentUser);
        }

        public async Task UpdateToken()
        {
            var tokenInfo = await _authService.RefreshTokenAsync(CurrentUser!.Id);

            CurrentToken = tokenInfo is null ? CurrentToken : tokenInfo;
        }

        public async Task UpdateClaims()
        {
            var claims = await _authService.GetUserClaimsByUserIdAsync(CurrentUser!.Id);

            Claims = claims is null ? Claims : claims;
        }

        public IEnumerable<string> GetUserRoles()
        {
            return _jwtPayloadService.GetRoles(CurrentToken!.Token);
        }


        private void OnClaimsChanged()
        {
            ClaimsChanged?.Invoke();
        }
    }
}
