using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;
using static OMS.UI.ViewModels.UserControls.FindPersonViewModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditUserViewModel : AddEditBaseViewModel<Models.UserModel, BL.Models.Tables.UserModel, IUserService>
    {
        private readonly IBranchService _branchService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private ObservableCollection<BranchOption> _branches = null!;

        public AddEditUserViewModel(IUserService userService, IBranchService branchService, IMapper mapper, IMessageService messageService,
                                    IFindPersonViewModel findPersonViewModel, IWindowService windowService, IStatusService statusService,
                                    IUserSessionService userSessionService) : base(userService, mapper, messageService, windowService, statusService)
        {
            _branchService = branchService;
            _userSessionService = userSessionService;
            _findPersonViewModel = findPersonViewModel;

            FindPersonViewModel.PersonFound += OnPersonFound;
            InitializeBranches();
        }

        private async void OnPersonFound(object? obj, PersonFoundEventArgs e)
        {
            if (Status.SelectMode == AddEditStatus.EnMode.Edit) return;

            var userId = await _service.GetIdByPersonIdAsync(e.PersonId);
            if (userId == 0) return;

            await EnterEditModeAsync(userId);
        }

        protected override async Task<BL.Models.Tables.UserModel?> GetByIdAsync(int id)
            => await _service.GetByIdAsync(id);

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            if (!await base.EnterEditModeAsync(id)) return false;

            LoadAssociatedPerson();
            return true;
        }

        protected override async Task Save(object? parameter)
        {
            if (!ValidatePersonSelection()) return;

            SetPersonReference();
            await base.Save(parameter);
        }

        protected override string GetEntityName()
            => "موظف";

        protected override async Task<bool> SaveDataAsync(bool isAdding, BL.Models.Tables.UserModel userDto)
            => isAdding
                ? await _service.AddAsync(userDto)
                : await _service.UpdateAsync(userDto);

        protected override void UpdateModelAfterSave(BL.Models.Tables.UserModel userDto)
            => Model.UserId = userDto.UserId;

        protected override bool ValidateModel()
        {
            if (!base.ValidateModel()) return false;
            if (!ValidatePersonSelection()) return false;
            if (!ValidateBranchSelection()) return false;

            return true;
        }

        protected override void SendMessage()
        {
            base.SendMessage();
            RefreshUserSession();
        }

        private async void InitializeBranches()
        {
            var branchOptionDto = await _branchService.GetAllBranchesOption();
            var branchOption = _mapper.Map<IEnumerable<BranchOption>>(branchOptionDto);
            Branches = new ObservableCollection<BranchOption>(branchOption);
        }

        private void LoadAssociatedPerson()
        {
            FindPersonViewModel.PersonId = Model.PersonId.ToString();
            FindPersonViewModel.FindPerson();
        }

        private bool ValidatePersonSelection()
        {
            if (FindPersonViewModel.Person != null) return true;

            ShowValidationError("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الشخص"));
            return false;
        }

        private bool ValidateBranchSelection()
        {
            if (Model.BranchId != 0) return true;

            ShowValidationError("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الفرع"));
            return false;
        }

        private void SetPersonReference()
            => Model.PersonId = FindPersonViewModel.Person!.PersonId;

        private void RefreshUserSession()
        {
            if (_userSessionService.CurrentUser?.PersonId == Model.PersonId)
                _userSessionService.UpdateModel();
        }

        private void ShowValidationError(string title, string message)
            => _messageService.ShowInfoMessage(title, message);
    }
}