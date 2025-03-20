using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditUserViewModel : AddEditBaseViewModel<UserModel, UserDto, IUserService>
    {
        private readonly IBranchOptionService _branchOptionService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private ObservableCollection<BranchOption> _branches;

        public AddEditUserViewModel(IUserService userService, IBranchOptionService branchOptionService, IMapper mapper,
                                    IFindPersonViewModel findPersonViewModel, IMessageService messageService,
                                    IWindowService windowService, IStatusService statusService, IUserSessionService userSessionService)
                                    : base(userService, mapper, messageService, windowService, statusService)
        {
            _findPersonViewModel = findPersonViewModel;
            _branchOptionService = branchOptionService;
            _userSessionService = userSessionService;

            Branches = new ObservableCollection<BranchOption>();
        }


        public override async Task<bool> OnOpeningDialog(int? id = -1)
        {
            await LoadBranchesAsync();
            return await base.OnOpeningDialog(id);
        }

        protected override async Task<UserDto?> GetByIdAsync(int userId)
            => await _service.GetByIdAsync(userId);

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            if (await base.EnterEditModeAsync(id))
            {
                LoadSelectedPerson();
                return true;
            }

            return false;
        }

        protected override async Task Save(object? parameter)
        {
            if (!ValidateSelectedPerson()) return;

            SetUserPersonId();

            await base.Save(parameter);
        }

        protected override async Task<bool> SaveDataAsync(bool isAdding, UserDto userDto)
            => isAdding ? await _service.AddAsync(userDto) : await _service.UpdateAsync(userDto);

        protected override void UpdateModelAfterSave(UserDto userDto)
            => Model.UserId = userDto.UserId;

        protected override string GetEntityName()
            => "موظف";

        protected override bool ValidateModel()
        {
            if (!ValidateFindPerson()) return false;

            if (!base.ValidateModel()) return false;

            return ValidateUser();
        }

        protected override void SendMessage()
        {
            base.SendMessage();
            if (_userSessionService.CurrentUser?.PersonId == Model.PersonId)
                _userSessionService.UpdateModel();
        }

        private async Task LoadBranchesAsync()
        {
            var branchOptionDto = await _branchOptionService.GetAllAsync();
            var branchOption = _mapper.Map<IEnumerable<BranchOption>>(branchOptionDto);
            Branches = new ObservableCollection<BranchOption>(branchOption);
        }

        private void LoadSelectedPerson()
        {
            FindPersonViewModel.PersonId = Model.PersonId.ToString();
            FindPersonViewModel.FindPerson();
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

        private void SetUserPersonId()
        {
            Model.PersonId = FindPersonViewModel.Person!.PersonId;
        }

        private bool ValidateFindPerson()
        {
            if (FindPersonViewModel.Person == null || FindPersonViewModel.Person.PersonId <= 0)
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الشخص الذي سوف يتم تفعيل الحساب عليه"));
                return false;
            }

            return true;
        }

        private bool ValidateUser()
        {
            if (Model.BranchId == 0)
            {
                _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الفرع الذي سوف يتم بناء الحساب عليه"));
                return false;
            }

            return true;
        }
    }
}