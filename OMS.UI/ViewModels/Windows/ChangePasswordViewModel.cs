using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Dtos.Hybrid;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Hash;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Windows;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ChangePasswordViewModel : ObservableValidator, IDialogInitializer<int?>
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly IWindowService _windowService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;


        [ObservableProperty]
        private bool _isModifiable = true;

        [Required(ErrorMessage = "تأكيد كلمة السر الجديدة مطلوبة")]
        [MinLength(5, ErrorMessage = "كلمة السر لا تقل عن خمس محارف")]
        [ObservableProperty]
        private string _newPasswordConfirm = null!;

        [ObservableProperty]
        private ChangePasswordModel _changePasswordModel = null!;

        public ChangePasswordViewModel(IUserService userService, IHashService hashService, IWindowService windowService,
                                       IMessageService messageService, IMapper mapper)
        {
            _userService = userService;
            _hashService = hashService;
            _windowService = windowService;
            _messageService = messageService;
            _mapper = mapper;
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

            var dto = _mapper.Map<ChangePasswordDto>(ChangePasswordModel);

            dto.OldPassword = _hashService.HashPassword(dto.OldPassword);
            dto.NewPassword = _hashService.HashPassword(dto.NewPassword);

            bool isChanged = await _userService.ChangePasswordAsync(dto);

            if (!isChanged)
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.PasswordResetErrorMessage);
                return;
            }

            
            _messageService.ShowInfoMessage("نجاح", MessageTemplates.PasswordResetSuccessMessage);
            IsModifiable = false;

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