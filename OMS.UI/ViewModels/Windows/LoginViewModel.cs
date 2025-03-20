using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.Models;
using OMS.UI.Models.Validations;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Authentication;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.Views;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.ViewModels.Windows
{
    public partial class LoginViewModel : ObservableValidator
    {
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserSessionService _userSessionService;


        [ObservableProperty]
        [Required(ErrorMessage = "نسيت كنابة اسم المستخدم")]
        [MinLength(3, ErrorMessage = "اسم المستخدم قصير")]
        [CustomValidation(typeof(UserValidation), nameof(UserValidation.ValidateUsername))]
        [NotifyDataErrorInfo]
        private string _username = string.Empty;

        [ObservableProperty]
        [Required(ErrorMessage = "نسيت كتابة كلمة السر")]
        [MinLength(5, ErrorMessage = "كلمة السر قصيرة")]
        [NotifyDataErrorInfo]
        private string _password = string.Empty;


        public LoginViewModel(IMessageService messageService, IWindowService windowService,
                              IAuthenticationService authenticationService, IUserSessionService userSessionService)
        {
            _messageService = messageService;
            _windowService = windowService;
            _authenticationService = authenticationService;
            _userSessionService = userSessionService;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (!ValidateInputs())
            {
                ShowValidationError(GetErrors()?.FirstOrDefault()?.ErrorMessage);
                return;
            }

            var user = await _authenticationService.AuthenticateAsync(Username, Password);

            if (!ValidateUserAccount(user)) return;

            _userSessionService.Login(user);

            _windowService.Hide();
            _windowService.Open<MainWindow>();
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

        private bool ValidateUserAccount(UserLoginModel? user)
        {
            if (user == null)
            {
                _messageService.ShowErrorMessage("حساب غير صالح", MessageTemplates.InvalidCredentialsErrorMessage);
                return false;
            }

            if (!user.IsActive)
            {
                _messageService.ShowErrorMessage("حالة الحساب", MessageTemplates.AccountInActiveErrorMessage);
                return false;
            }

            return true;
        }
    }
}