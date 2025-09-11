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
            SelectedItemChanged += NotifyCanExecuteChanged;
        }

        private void NotifyCanExecuteChanged(object? obj, EventArgs e)
        {
            ActiveUserCommand.NotifyCanExecuteChanged();
            InActiveUserCommand.NotifyCanExecuteChanged();
            OpenChangePasswordCommand.NotifyCanExecuteChanged();
            OpenUserRolesManagerCommand.NotifyCanExecuteChanged();
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(UserDetailModel item)
            => item.Id;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditUserWindow, (int? UserId, bool IsOpendOnUsersPage)>((itemId, true));

        protected override async Task<UserDetailModel> ConvertToModel(UserModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.Id))!;
        }

        protected override async Task DeleteItem()
        {
            if (SelectedItem == null) return;

            if (_userSessionService.CurrentUser?.Id == SelectedItem?.Id)
            {
                _messageService.ShowErrorMessage("اجراء منع", MessageTemplates.InvalidDeleteUserMessage);
                return;
            }

            await base.DeleteItem();
        }

        [RelayCommand(CanExecute = nameof(CanActiveUser))]
        private async Task ActiveUser()
        {
            await UpdateUserActivation(MessageTemplates.ActivateUserConfirmation, true, "تم التفعيل");
            ActiveUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanActiveUser()
        {
            return SelectedItem?.IsActive == false && _userSessionService.Claims!.Contains(PermissionsData.Users.Activation);
        }

        [RelayCommand(CanExecute = nameof(CanInActiveUser))]
        private async Task InActiveUser()
        {
            if (_userSessionService.CurrentUser?.Id == SelectedItem?.Id)
            {
                _messageService.ShowInfoMessage("منع الوصول", MessageTemplates.AccountCantInActive);
                return;
            }

            await UpdateUserActivation(MessageTemplates.InActivateUserConfirmation, false, "تم الغاء التفعيل");
            InActiveUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanInActiveUser()
        {
            return SelectedItem?.IsActive == true && _userSessionService.Claims!.Contains(PermissionsData.Users.Activation);
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
            await _dialogService.ShowDialog<ChangePasswordWindow, int?>(SelectedItem?.Id);
        }

        private bool CanOpenChangePassword()
        {
            return SelectedItem != null && _userSessionService.Claims!.Contains(PermissionsData.Users.ChangePassword);
        }

        private async Task UpdateUserActivation(string userConfirmationMessage, bool isActiveUser, string successTitle)
        {
            if (!_messageService.ShowQuestionMessage("تنويه", userConfirmationMessage))
                return;

            bool isSuccess = await _service.UpdateUserActivationStatus(SelectedItem!.Id, isActiveUser);

            if (isSuccess)
            {
                SelectedItem!.IsActive = isActiveUser;
                _messageService.ShowInfoMessage(successTitle, MessageTemplates.SuccessMessage);

                ActiveUserCommand.NotifyCanExecuteChanged();
                InActiveUserCommand.NotifyCanExecuteChanged();
            }
        }

    }
}
