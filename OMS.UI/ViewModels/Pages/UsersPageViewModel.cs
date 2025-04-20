using AutoMapper;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
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

        public UsersPageViewModel(IUserService userService, IUserDetailService userDetailService, IMapper mapper,
                                  IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService) : base(userService, userDetailService, mapper, dialogService, messageService)
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
            Items = new(_mapper.Map<IEnumerable<UserDetailModel>>(usersData));
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditUserWindow, int?>(itemId);

        protected override async Task<UserDetailModel> ConvertToModel(UserModel messageModel)
        {
            var userDto = await _displayService.GetByIdAsync(messageModel.UserId);
            return _mapper.Map<UserDetailModel>(userDto);
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
