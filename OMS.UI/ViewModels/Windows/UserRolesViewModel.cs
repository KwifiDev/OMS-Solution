using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class UserRolesViewModel : ObservableObject, IDialogInitializer<UserDetailModel>
    {
        private readonly IRoleService _roleService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private UserDetailModel _currentUser = null!;

        [ObservableProperty]
        private ObservableCollection<UserRoleSelectionModel> _availableRoles = null!;

        private IEnumerable<string> _currentUserRoles = Enumerable.Empty<string>();

        public UserRolesViewModel(IRoleService roleService, IAuthService authService, IMapper mapper,
                                  IMessageService messageService, IWindowService windowService, IUserSessionService userSessionService)
        {
            _roleService = roleService;
            _authService = authService;
            _mapper = mapper;
            _messageService = messageService;
            _windowService = windowService;
            _userSessionService = userSessionService;
        }

        public async Task<bool> OnOpeningDialog(UserDetailModel? userModel)
        {
            if (userModel is null) return false;

            CurrentUser = userModel;
            
            return await LoadData();
        }


        [RelayCommand]
        private async Task<bool> LoadData()
        {
            try
            {
                var allRoles = await _roleService.GetAllAsync();
                AvailableRoles = new(_mapper.Map<IEnumerable<UserRoleSelectionModel>>(allRoles));
                await LoadCurrentUserRolesAsync();
                return AvailableRoles.Count > 0;
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage("خطأ في تحميل البيانات", $"حدث خطأ أثناء تحميل الأدوار: {ex.Message}");
                return false;
            }
        }

        private async Task LoadCurrentUserRolesAsync()
        {
            _currentUserRoles = await _authService.GetUserRolesByUserIdAsync(CurrentUser.UserId);

            if (!_currentUserRoles.Any()) return;

            foreach (var role in AvailableRoles)
            {
                role.IsSelected = _currentUserRoles.Contains(role.RoleName);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSaveRoleChanges))]
        private async Task SaveRoleChangesAsync()
        {
            var (rolesToAdd, rolesToRemove) = CalculateRoleChanges();
            await ApplyRoleChangesAsync(rolesToAdd, rolesToRemove);

            if (_userSessionService.CurrentUser!.UserId == CurrentUser.UserId)
            {
                await _userSessionService.UpdateClaims();
                await _userSessionService.UpdateToken();
            }
        }

        private bool CanSaveRoleChanges()
        {
            return _userSessionService.Claims!.Contains(PermissionsData.Users.ManageRoles);
        }

        private (ICollection<string> rolesToAdd, ICollection<string> rolesToRemove) CalculateRoleChanges()
        {
            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            foreach (var role in AvailableRoles)
            {
                bool wasAssigned = _currentUserRoles.Contains(role.RoleName);
                bool nowAssigned = role.IsSelected;

                if (!wasAssigned && nowAssigned)
                {
                    rolesToAdd.Add(role.RoleName);
                }
                else if (wasAssigned && !nowAssigned)
                {
                    rolesToRemove.Add(role.RoleName);
                }
            }

            return (rolesToAdd, rolesToRemove);
        }

        private async Task ApplyRoleChangesAsync(ICollection<string> rolesToAdd, ICollection<string> rolesToRemove)
        {
            try
            {
                bool isSuccess = await _authService.ChangeUserRolesAsync(CurrentUser.UserId, rolesToAdd, rolesToRemove);

                if (isSuccess)
                {
                    _messageService.ShowInfoMessage("تعيين الادوار", MessageTemplates.AssignRolesSuccessMessage);
                    _currentUserRoles = await _authService.GetUserRolesByUserIdAsync(CurrentUser.UserId);
                }
                else
                {
                    _messageService.ShowErrorMessage("تعيين الادوار", MessageTemplates.AssignRolesErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage("خطأ في الحفظ", $"حدث خطأ أثناء حفظ التغييرات: {ex.Message}");
            }
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }


    }
}