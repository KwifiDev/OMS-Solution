using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class UsersPageViewModel : BasePageViewModel<IUserService, UserDetailModel>
    {
        private readonly IUserDetailService _userViewService;

        public UsersPageViewModel(IUserService userService, IUserDetailService userViewService, IMapper mapper,
                                  IDialogService dialogService, IMessageService messageService) : base(userService, mapper, dialogService, messageService)
        {
            _userViewService = userViewService;
            WeakReferenceMessenger.Default.Register<IMessage<UserModel>>(this, OnUserMessageReceived);
        }

        protected override void OnMessageReceived(object recipient, IMessage<UserDetailModel> message) { }

        private async void OnUserMessageReceived(object recipient, IMessage<UserModel> message)
        {
            switch (message.Status.Operation)
            {
                case AddEditStatus.EnExecuteOperation.Added:
                    await HandleUserAdd(message.Model);
                    break;
                case AddEditStatus.EnExecuteOperation.Updated:
                    await HandleUserUpdate(message.Model);
                    break;
            }
        }

        private async Task HandleUserAdd(UserModel? user)
        {
            if (user == null) return;
            Items.Add(await ConvertToUserDetailModel(user));
        }

        private async Task HandleUserUpdate(UserModel? user)
        {
            if (user == null || SelectedItem == null) return;

            var index = Items.IndexOf(SelectedItem);
            if (index >= 0) Items[index] = await ConvertToUserDetailModel(user);
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(UserDetailModel item)
            => item.UserId;

        protected override async Task LoadData()
        {
            var usersData = await _userViewService.GetAllAsync();
            Items = new(_mapper.Map<IEnumerable<UserDetailModel>>(usersData));
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditUserWindow>(itemId);

        private async Task<UserDetailModel> ConvertToUserDetailModel(UserModel user)
        {
            var userDto = await _userViewService.GetByIdAsync(user.UserId);
            return _mapper.Map<UserDetailModel>(userDto);
        }
    }
}
