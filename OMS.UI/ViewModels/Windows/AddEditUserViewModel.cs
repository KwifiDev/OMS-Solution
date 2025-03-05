using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ModelTransfer;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AddEditUserViewModel : ObservableObject, IDialogInitializer
    {
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;
        private readonly IBranchOptionService _branchOptionService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private UserModel _user = null!;

        [ObservableProperty]
        private AddEditStatus _status;

        [ObservableProperty]
        private ObservableCollection<BranchOption> _branches;

        public AddEditUserViewModel(IUserService userService, IBranchOptionService branchOptionService, IMapper mapper,
                                    IFindPersonViewModel findPersonViewModel, IMessageService messageService,
                                    IWindowService windowService, IStatusService statusService)
        {
            _findPersonViewModel = findPersonViewModel;
            _messageService = messageService;
            _windowService = windowService;
            _branchOptionService = branchOptionService;
            _mapper = mapper;
            _userService = userService;

            Branches = new ObservableCollection<BranchOption>();
            Status = statusService.CreateAddEditStatus();
        }

        public async Task<bool> Initialize(int? userId = -1)
        {
            try
            {
                await LoadBranchesAsync();
                return userId > 0 ? await EnterEditModeAsync(userId) : EnterAddMode();
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage("خطأ في التهيئة", ex.Message);
                return false;
            }
        }

        private bool EnterAddMode()
        {
            Status.SelectMode = AddEditStatus.EnMode.Add;

            User = new UserModel();
            return true;
        }

        private async Task<bool> EnterEditModeAsync(int? userId)
        {
            if (userId == null)
            {
                _messageService.ShowErrorMessage("خطأ", MessageTemplates.ErrorMessage);
                return false;
            }

            var userDto = await _userService.GetByIdAsync((int)userId);
            if (userDto == null)
            {
                _messageService.ShowErrorMessage("اجراء البحث عن موظف", MessageTemplates.SearchErrorMessage);
                return false;
            }

            Status.SelectMode = AddEditStatus.EnMode.Edit;

            User = _mapper.Map<UserModel>(userDto);

            LoadSelectedPerson();

            return true;
        }

        private void LoadSelectedPerson()
        {
            FindPersonViewModel.PersonId = User.PersonId.ToString();
            FindPersonViewModel.FindPerson();
        }

        private async Task LoadBranchesAsync()
        {
            var branchOptionDto = await _branchOptionService.GetAllAsync();
            var branchOption = _mapper.Map<IEnumerable<BranchOption>>(branchOptionDto);
            Branches = new ObservableCollection<BranchOption>(branchOption);
        }

        private void SetUserPersonId()
        {
            User.PersonId = FindPersonViewModel.Person!.PersonId;
        }

        private void UpdateStatusAndNotifyUser(bool isAdding, UserDto userDto)
        {
            if (isAdding)
            {
                User.UserId = userDto.UserId;
                Status.Operation = AddEditStatus.EnExecuteOperation.Added;

                _messageService.ShowInfoMessage("اجراء اضافة موظف جديد", MessageTemplates.AdditionSuccessMessage);
            }
            else
            {
                Status.Operation = AddEditStatus.EnExecuteOperation.Updated;

                _messageService.ShowInfoMessage("اجراء تعديل بيانات شخص", MessageTemplates.UpdateSuccessMessage);
            }

            SendMessage();

        }

        private void SendMessage()
        {
            var message = new ModelTransferService<UserModel> { Model = User, Status = Status };
            WeakReferenceMessenger.Default.Send<IMessage<UserModel>>(message);
        }

        private async Task<bool> SaveUserDataAsync(bool isAdding, UserDto userDto)
        {
            return isAdding
                ? await _userService.AddAsync(userDto)
                : await _userService.UpdateAsync(userDto);
        }

        private bool ValidateSelectedPerson()
        {
            if (FindPersonViewModel.Person == null)
            {
                _messageService.ShowErrorMessage("شخص غير محدد", MessageTemplates.SaveErrorMessage);
                return false;
            }

            return true;
        }

        private bool ValidateUser()
        {
            if (FindPersonViewModel.Person == null || FindPersonViewModel.Person.PersonId <= 0)
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الشخص الذي سوف يتم تفعيل الحساب عليه"));
                return false;
            }

            if (!User.ArePropertiesValid())
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(User.GetErrors()?.FirstOrDefault()?.ErrorMessage));
                return false;
            }
            return true;
        }

        private UserDto MapUserModelToDto()
        {
            return _mapper.Map<UserDto>(User);
        }


        [RelayCommand]
        private async Task SaveUser(object? parameter)
        {
            if (!ValidateSelectedPerson()) return;

            SetUserPersonId();

            if (!ValidateUser()) return;

            var userDto = MapUserModelToDto();
            var isAdding = Status.SelectMode == AddEditStatus.EnMode.Add;

            bool isSuccess = await SaveUserDataAsync(isAdding, userDto);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("اجراء حفظ بيانات شخص", MessageTemplates.SaveErrorMessage);
                return;
            }

            UpdateStatusAndNotifyUser(isAdding, userDto);
            Status.IsModifiable = false;
            Status.ModelObject = User;
        }

        [RelayCommand]
        public void Close()
        {
            _windowService.Close();
        }

        [RelayCommand]
        private void DragWindow()
        {
            _windowService.DragMove();
        }
    }
}