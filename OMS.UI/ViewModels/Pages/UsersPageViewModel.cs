using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.DA.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.Status;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public partial class UsersPageViewModel : ObservableObject
    {
        private readonly IUserDetailService _userService;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;

        [ObservableProperty]
        private ObservableCollection<UserDetailModel>? _users;

        [ObservableProperty]
        private UserDetailModel? _selectedUser;

        public UsersPageViewModel(IUserDetailService userService, IMapper mapper, IDialogService dialogService, IMessageService messageService)
        {
            _userService = userService;
            _mapper = mapper;
            _dialogService = dialogService;
            _messageService = messageService;

            WeakReferenceMessenger.Default.Register<IMessage<UserDetailModel>>(this, OnMessageReceived);
        }

        private void OnMessageReceived(object recipient, IMessage<UserDetailModel> message)
        {
            switch (message.Status.Operation)
            {
                case StatusInfo.EnExecuteOperation.Added:
                    OnUserAdd(message.Model);
                    break;
                case StatusInfo.EnExecuteOperation.Updated:
                    OnUserEdit(message.Model);
                    break;
                default:
                    // Handle other cases if needed
                    break;
            }
        }

        //[RelayCommand]
        //private async Task ShowDetails(UserDetailsModel? user)
        //{
        //    if (user == null) return;
        //    SelectItem(user);

        //    await _dialogService.ShowDialog<PersonDetailsWindow>(user.UserId);
        //}

        //[RelayCommand]
        //private async Task AddUser()
        //{
        //    await _dialogService.ShowDialog<AddEditPersonWindow>();
        //}

        //[RelayCommand]
        //private async Task EditUser(UserDetailsModel? user)
        //{
        //    if (user == null) return;
        //    SelectItem(user);

        //    await _dialogService.ShowDialog<AddEditPersonWindow>(user.UserId);
        //}

        //[RelayCommand]
        //private async Task DeleteUser(UserDetailsModel? user)
        //{
        //    if (user == null) return;
        //    SelectItem(user);

        //    if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.DeletionConfirmation))
        //        return;

        //    bool isDeleted = await _userService.DeleteAsync(user.UserId);
        //    if (isDeleted)
        //    {
        //        Users!.Remove(SelectedUser!);
        //        _messageService.ShowInfoMessage("اجراء حذف", MessageTemplates.DeletionSuccessMessage);
        //    }
        //    else
        //    {
        //        _messageService.ShowErrorMessage("خطأ", MessageTemplates.DeletionErrorMessage);
        //    }
        //}

        private void OnUserAdd(UserDetailModel? user)
        {
            if (user == null) return;
            UserDetailModel? userModel = user as UserDetailModel;

            Users!.Add(userModel!);
        }

        private void OnUserEdit(UserDetailModel? user)
        {
            if (user == null) return;
            UserDetailModel? userModel = user as UserDetailModel;

            int userIndex = Users!.IndexOf(SelectedUser!);
            Users[userIndex] = userModel!;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var usersDto = await _userService.GetAllAsync();
            var users = _mapper.Map<IEnumerable<UserDetailModel>>(usersDto);
            Users = new ObservableCollection<UserDetailModel>(users ?? Enumerable.Empty<UserDetailModel>());
        }

        //private void SelectItem(UserModel person)
        //{
        //    SelectedUser = person;
        //}
    }
}
