using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Views.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public partial class UsersPageViewModel : ObservableObject
    {
        private readonly IUserDetailService _userViewService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private ObservableCollection<UserDetailModel>? _users;

        [ObservableProperty]
        private UserDetailModel? _selectedUser;

        public UsersPageViewModel(IUserService userService, IUserDetailService userViewService, IMapper mapper, IDialogService dialogService, IMessageService messageService)
        {
            _userViewService = userViewService;
            _userService = userService;
            _mapper = mapper;
            _dialogService = dialogService;
            _messageService = messageService;

            WeakReferenceMessenger.Default.Register<IMessage<UserModel>>(this, OnMessageReceived);
        }

        private void OnMessageReceived(object recipient, IMessage<UserModel> message)
        {
            switch (message.Status.Operation)
            {
                case AddEditStatus.EnExecuteOperation.Added:
                    OnUserAdd(message.Model);
                    break;
                case AddEditStatus.EnExecuteOperation.Updated:
                    OnUserEdit(message.Model);
                    break;
                default:
                    // Handle other cases if needed
                    break;
            }
        }

        private async void OnUserAdd(UserModel? user)
        {
            if (user == null) return;

            UserDetailModel? userModel = await ConvertToUserDetailModel(user);

            Users!.Add(userModel!);
        }

        private async void OnUserEdit(UserModel? user)
        {
            if (user == null) return;
            UserDetailModel? userModel = await ConvertToUserDetailModel(user);

            int userIndex = Users!.IndexOf(SelectedUser!);
            Users[userIndex] = userModel!;
        }

        private async Task<UserDetailModel?> ConvertToUserDetailModel(UserModel user)
        {
            var userDto = await _userViewService.GetByIdAsync(user.UserId);
            return _mapper.Map<UserDetailModel?>(userDto);
        }

        private void SelectItem(UserDetailModel user)
        {
            SelectedUser = user;
        }


        [RelayCommand]
        private async Task ShowDetails(UserDetailModel? user)
        {
            if (user == null) return;
            SelectItem(user);

            await Task.Run(() => user);
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
        }

        [RelayCommand]
        private async Task AddUser()
        {
            await _dialogService.ShowDialog<AddEditUserWindow>();
        }

        [RelayCommand]
        private async Task EditUser(UserDetailModel? user)
        {
            if (user == null) return;
            SelectItem(user);

            await _dialogService.ShowDialog<AddEditUserWindow>(user.UserId);
        }

        [RelayCommand]
        private async Task DeleteUser(UserDetailModel? user)
        {
            if (user == null) return;
            SelectItem(user);

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.DeletionConfirmation))
                return;

            bool isDeleted = await _userService.DeleteAsync(user.UserId);
            if (isDeleted)
            {
                Users!.Remove(SelectedUser!);
                _messageService.ShowInfoMessage("اجراء حذف", MessageTemplates.DeletionSuccessMessage);
            }
            else
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.DeletionErrorMessage);
            }
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var usersDto = await _userViewService.GetAllAsync();
            var users = _mapper.Map<IEnumerable<UserDetailModel>>(usersDto);
            Users = new ObservableCollection<UserDetailModel>(users ?? Enumerable.Empty<UserDetailModel>());
        }

    }
}
