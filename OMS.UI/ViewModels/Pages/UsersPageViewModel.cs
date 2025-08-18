using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class UsersPageViewModel : BasePageViewModel<IUserService, IUserDetailService, UserDetailModel, UserModel>
    {
        protected override string ViewClaim => PermissionsData.UsersDetail.View;
        protected override string AddClaim => PermissionsData.Users.Add;
        protected override string EditClaim => PermissionsData.Users.Edit;
        protected override string DeleteClaim => PermissionsData.Users.Delete;

        public UsersPageViewModel(IUserService userService, IUserDetailService userDetailService, ILoadingService loadingService,
                                  IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService)
                                  : base(userService, userDetailService, loadingService, dialogService, messageService, userSessionService)
        {
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(UserDetailModel item)
            => item.UserId;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditUserWindow, int?>(itemId);

        protected override async Task<UserDetailModel> ConvertToModel(UserModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.UserId))!;
        }

        protected override async Task DeleteItem()
        {
            if (SelectedItem == null) return;

            if (_userSessionService.CurrentUser?.UserId == SelectedItem?.UserId)
            {
                _messageService.ShowErrorMessage("اجراء منع", MessageTemplates.InvalidDeleteUserMessage);
                return;
            }

            await base.DeleteItem();
        }

        [RelayCommand(CanExecute = nameof(CanActiveUser))]
        private async Task ActiveUser()
        {
            await UpdateUserActivation(MessageTemplates.ActivateUserConfirmation, true, "فعّال", "تم التفعيل");
            ActiveUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanActiveUser()
        {
            return SelectedItem?.IsActive == "غير فعّال" && _userSessionService.Claims!.Contains(PermissionsData.Users.Activation);
        }

        [RelayCommand(CanExecute = nameof(CanInActiveUser))]
        private async Task InActiveUser()
        {
            if (_userSessionService.CurrentUser?.UserId == SelectedItem?.UserId)
            {
                _messageService.ShowInfoMessage("منع الوصول", MessageTemplates.AccountCantInActive);
                return;
            }

            await UpdateUserActivation(MessageTemplates.InActivateUserConfirmation, false, "غير فعّال", "تم الغاء التفعيل");
            InActiveUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanInActiveUser()
        {
            return SelectedItem?.IsActive == "فعّال" && _userSessionService.Claims!.Contains(PermissionsData.Users.Activation);
        }

        [RelayCommand(CanExecute = nameof(CanOpenRolesManager))]
        private async Task OpenUserRolesManager()
        {
            await _dialogService.ShowDialog<UserRolesWindow, UserDetailModel>(SelectedItem);
        }

        private bool CanOpenRolesManager()
        {
            return SelectedItem != null && _userSessionService.Claims!.Contains(PermissionsData.Users.ManageRoles);
        }


        [RelayCommand(CanExecute = nameof(CanOpenChangePassword))]
        private async Task OpenChangePassword()
        {
            await _dialogService.ShowDialog<ChangePasswordWindow, int?>(SelectedItem?.UserId);
        }

        private bool CanOpenChangePassword()
        {
            return SelectedItem != null && _userSessionService.Claims!.Contains(PermissionsData.Users.ChangePassword);
        }

        private async Task UpdateUserActivation(string userConfirmationMessage, bool isActiveUser, string isActiveTitle, string successTitle)
        {
            if (!_messageService.ShowQuestionMessage("تنويه", userConfirmationMessage))
                return;

            bool isSuccess = await _service.UpdateUserActivationStatus(SelectedItem!.UserId, isActiveUser);

            if (isSuccess)
            {
                SelectedItem!.IsActive = isActiveTitle;
                _messageService.ShowInfoMessage(successTitle, MessageTemplates.SuccessMessage);

                ActiveUserCommand.NotifyCanExecuteChanged();
                InActiveUserCommand.NotifyCanExecuteChanged();
            }
        }

    }
}
