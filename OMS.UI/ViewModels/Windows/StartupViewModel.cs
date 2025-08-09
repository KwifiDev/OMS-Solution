using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Connection;
using OMS.UI.Models.Others;
using OMS.UI.Services.Authentication;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.Views;
using OMS.UI.Views.Windows;
using System.Windows;
using static OMS.UI.Services.Authentication.AuthenticationService;

namespace OMS.UI.ViewModels.Windows
{
    public partial class StartupViewModel : ObservableObject
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRegistryService _regestryService;
        private readonly IWindowService _windowService;
        private readonly IMessageService _messageService;
        private readonly IUserSessionService _userSessionService;
        private readonly IConnectionService _connectionService;


        [ObservableProperty]
        private string _loadingMessage = null!;

        public StartupViewModel(IAuthenticationService authenticationService, IRegistryService regestryService, IWindowService windowService,
                                IMessageService messageService, IUserSessionService userSessionService, IConnectionService connectionService)
        {
            _authenticationService = authenticationService;
            _regestryService = regestryService;
            _windowService = windowService;
            _messageService = messageService;
            _userSessionService = userSessionService;
            _connectionService = connectionService;
        }

        [RelayCommand]
        public async Task OnStartup()
        {
            await SetLoadingMessage("الرجاء الانتظار...");

            try
            {
                await SetLoadingMessage("جاري الاتصال في الخادم");
                await CheckServerConnectionAsync();

                await HandleUserAuthenticationAsync();
            }
            catch (Exception ex)
            {
                HandleStartupError(ex);
            }
        }

        private async Task CheckServerConnectionAsync()
        {
            var isConnected = await _connectionService.VerifyServerConnectionAsync();

            if (!isConnected) _windowService.Exit();
        }

        private (bool hasCredentials, string? username, string? password) CheckForSavedCredentials()
        {
            _regestryService.GetUserLoginConfig(out string? username, out string? password);

            return (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password), username, password);
        }

        private async Task<(bool IsAuthenticated, UserLoginModel? User)> TryAuthenticateWithSavedCredentialsAsync(string username, string password)
        {
            try
            {
                var user = await _authenticationService.AuthenticateAsync(username, password);
                var validationStatus = _authenticationService.ValidateUserAccount(user);

                return (validationStatus == EnUserValidateStatus.FoundAndActive, user);
            }
            catch
            {
                return (false, null);
            }
        }

        private async Task HandleUserAuthenticationAsync()
        {
            var (hasSavedCredentials, username, password) = CheckForSavedCredentials();

            if (!hasSavedCredentials)
            {
                await SetLoadingMessage("فتح نافذة تسجيل الدخول");

                SwitchWindow<LoginWindow>();
                return;
            }

            await SetLoadingMessage("جاري التحقق من الاعتماديات");
            var authenticationResult = await TryAuthenticateWithSavedCredentialsAsync(username!, password!);

            if (authenticationResult.IsAuthenticated)
            {
                await SetLoadingMessage("جاري تسجيل الدخول, الرجاء الانتظار...");
                _userSessionService.Login(authenticationResult.User!, password!, true);

                SwitchWindow<MainWindow>();
            }
            else
            {
                await SetLoadingMessage("بيانات الاعتماد غير صحيحة");
                
                _regestryService.ResetUserLoginConfig();
                await SetLoadingMessage("تم اعادة تعيين الاعتماديات");

                SwitchWindow<LoginWindow>();
            }
        }

        private void SwitchWindow<TWindow>() where TWindow : Window
        {
            _windowService.HideStartupWindow();
            _windowService.Open<TWindow>();
        }

        private async Task SetLoadingMessage(string message, int milisecounds = 2000)
        {
            LoadingMessage = message;
            await Task.Delay(milisecounds);
        }

        private void HandleStartupError(Exception ex)
        {
            _messageService.ShowErrorMessage("Startup Error", $"Application failed to start: {ex.Message}");
            _windowService.Exit();
        }
    }
}