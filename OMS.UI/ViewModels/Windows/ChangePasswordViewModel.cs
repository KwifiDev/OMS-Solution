using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Hash;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ChangePasswordViewModel : ObservableValidator, IDialogInitializer<int?>
    {
        private readonly IAuthService _authService;
        private readonly IHashService _hashService;
        private readonly IWindowService _windowService;
        private readonly IRegistryService _registryService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUserSessionService _userSessionService;


        [ObservableProperty]
        private bool _isModifiable = true;

        [Required(ErrorMessage = "تأكيد كلمة السر الجديدة مطلوبة")]
        [MinLength(5, ErrorMessage = "كلمة السر لا تقل عن خمس محارف")]
        [ObservableProperty]
        private string _newPasswordConfirm = null!;

        [ObservableProperty]
        private ChangePasswordModel _changePasswordModel = null!;

        public ChangePasswordViewModel(IAuthService authService, IHashService hashService, IWindowService windowService, IRegistryService registryService,
                                       IMessageService messageService, IMapper mapper, IUserSessionService userSessionService)
        {
            _authService = authService;
            _hashService = hashService;
            _windowService = windowService;
            _registryService = registryService;
            _messageService = messageService;
            _mapper = mapper;
            _userSessionService = userSessionService;
        }

        public async Task<bool> OnOpeningDialog(int? userId)
        {
            if (userId is null || userId < 0) return false;

            ChangePasswordModel = new ChangePasswordModel { UserId = (int)userId };

            return await Task.FromResult(userId > 0);
        }

        [RelayCommand]
        private async Task ChangePassword()
        {
            if (!ChangePasswordModel.ArePropertiesValid()) return;
            if (!IsNewPasswordMatched()) return;

            var model = new ChangePasswordModel
            {
                UserId = ChangePasswordModel.UserId,
                OldPassword = ChangePasswordModel.OldPassword,
                NewPassword = ChangePasswordModel.NewPassword
            };

            //HashPass(model);

            bool isChanged = await _authService.ChangePasswordAsync(model);

            if (!isChanged)
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.PasswordResetErrorMessage);
                return;
            }

            SaveNewPasswordConifg(model.NewPassword);

            _messageService.ShowInfoMessage("نجاح", MessageTemplates.PasswordResetSuccessMessage);
            IsModifiable = false;

        }

        //private void HashPass(ChangePasswordModel model)
        //{
        //    model.OldPassword = _hashService.HashPassword(model.OldPassword);
        //    model.NewPassword = _hashService.HashPassword(model.NewPassword);
        //}

        private void SaveNewPasswordConifg(string newPassword)
        {
            if (ChangePasswordModel.UserId == _userSessionService.CurrentUser?.UserId)
            {
                _registryService.GetUserLoginConfig(out string? username, out string? password);
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    _registryService.SetUserLoginConfig(username, newPassword);
                }
            }

        }

        private bool IsNewPasswordMatched()
        {
            if (ChangePasswordModel.NewPassword != NewPasswordConfirm)
            {
                _messageService.ShowInfoMessage("العملية", MessageTemplates.PasswordMismatchMessage);
                return false;
            }

            return true;
        }

        [RelayCommand]
        private void Close() => _windowService.Close();

    }
}