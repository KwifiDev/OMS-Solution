using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Models.Validations;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Authentication;
using OMS.UI.Services.Hash;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.Views;
using System.ComponentModel.DataAnnotations;
using static OMS.UI.Services.Authentication.AuthenticationService;

namespace OMS.UI.ViewModels.Windows
{
    public partial class LoginViewModel : ObservableValidator
    {
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;
        private readonly IHashService _hashService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserSessionService _userSessionService;
        private readonly IRegistryService _registryService;
        private bool _isLoading;


        [ObservableProperty]
        [Required(ErrorMessage = "نسيت كنابة اسم المستخدم")]
        [MinLength(3, ErrorMessage = "اسم المستخدم قصير")]
        [CustomValidation(typeof(UserValidation), nameof(UserValidation.ValidateUsername))]
        [NotifyDataErrorInfo]
        private string _username = string.Empty;

        [ObservableProperty]
        [Required(ErrorMessage = "نسيت كتابة كلمة السر")]
        [MinLength(6, ErrorMessage = "كلمة السر قصيرة")]
        [NotifyDataErrorInfo]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _loginButtonContent = "تسجيل الدخول";

        [ObservableProperty]
        private bool _isRememberUserLogin;

        public LoginViewModel(IMessageService messageService, IWindowService windowService, IHashService hashService,
                              IAuthenticationService authenticationService, IUserSessionService userSessionService, IRegistryService registryService)
        {
            _messageService = messageService;
            _windowService = windowService;
            _hashService = hashService;
            _authenticationService = authenticationService;
            _userSessionService = userSessionService;
            _registryService = registryService;
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
                LoginButtonContent = _isLoading ? "جاري التحقق..." : "تسجيل الدخول";
            }
        }


        [RelayCommand]
        private async Task Login()
        {
            if (!ValidateInputs())
            {
                ShowValidationError(GetErrors()?.FirstOrDefault()?.ErrorMessage);
                return;
            }

            IsLoading = true;

            var loginInfo = await _authenticationService.AuthenticateAsync(Username, Password);

            var validationStatus = _authenticationService.ValidateUserAccount(loginInfo);

            if (!CheckUserAccountStatus(validationStatus)) { IsLoading = false; return; }

            _userSessionService.Login(loginInfo, Password, IsRememberUserLogin);

            _windowService.HideLoginWindow();
            _windowService.Open<MainWindow>();

            IsLoading = false;
        }


        [RelayCommand]
        private void Exit() => _windowService.Exit();

        private bool ValidateInputs()
        {
            ValidateAllProperties();
            return !HasErrors;
        }

        private void ShowValidationError(string? error) =>
            _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(error));

        private bool CheckUserAccountStatus(EnUserValidateStatus validationStatus)
        {
            switch (validationStatus)
            {
                case EnUserValidateStatus.NotFound:
                    _messageService.ShowErrorMessage("حساب غير صالح", MessageTemplates.InvalidCredentialsErrorMessage);
                    return false;

                case EnUserValidateStatus.NotActive:
                    _messageService.ShowErrorMessage("حالة الحساب", MessageTemplates.AccountInActiveErrorMessage);
                    return false;
            }

            return true;
        }
    }
}