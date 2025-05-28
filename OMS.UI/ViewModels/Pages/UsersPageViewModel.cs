using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.UserSession;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class UsersPageViewModel : BasePageViewModel<IUserService, IUserDetailService, UserDetailModel, UserModel>
    {
        private readonly IUserSessionService _userSessionService;

        public UsersPageViewModel(IUserService userService, IUserDetailService userDetailService, IDialogService dialogService,
                                  IMessageService messageService, IUserSessionService userSessionService) :
                                  base(userService, userDetailService, dialogService, messageService)
        {
            _userSessionService = userSessionService;
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(UserDetailModel item)
            => item.UserId;

        protected override async Task LoadData()
        {
            var usersData = await _displayService.GetAllAsync();
            Items = new(usersData);
        }

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

    }
}
