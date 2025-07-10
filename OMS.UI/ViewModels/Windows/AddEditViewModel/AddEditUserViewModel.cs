using CommunityToolkit.Mvvm.ComponentModel;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;
using OMS.UI.Models.Validations;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Hash;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using static OMS.UI.ViewModels.UserControls.FindPersonViewModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditUserViewModel : AddEditBaseViewModel<UserModel, IUserService>
    {
        private readonly IBranchService _branchService;
        private readonly IUserSessionService _userSessionService;
        private readonly IHashService _hashService;
        private readonly IRegistryService _registryService;

        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private ObservableCollection<BranchOptionModel> _branches = null!;

        public AddEditUserViewModel(IUserService userService, IBranchService branchService, IMessageService messageService, IRegistryService registryService,
                                    IFindPersonViewModel findPersonViewModel, IWindowService windowService, IStatusService statusService,
                                    IUserSessionService userSessionService, IHashService hashService) : base(userService, messageService, windowService, statusService)
        {
            _branchService = branchService;
            _userSessionService = userSessionService;
            _findPersonViewModel = findPersonViewModel;
            _hashService = hashService;
            _registryService = registryService;

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

        protected override async Task<UserModel?> GetByIdAsync(int id)
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
            if (!await ValidateUsername()) return;

            SetPersonReference();
            await base.Save(parameter);
        }

        protected override string GetEntityName()
            => "موظف";

        protected override async Task<bool> SaveDataAsync(bool isAdding, UserModel userModel)
        {
            if (isAdding)
            {
                userModel.Password = _hashService.HashPassword(userModel.Password);
                return await _service.AddAsync(userModel);
            }
            else
            {
                bool isUpdated = await _service.UpdateAsync(userModel.UserId, userModel);
                if (isUpdated) UpdateUsernameConfig(userModel.Username);
                return isUpdated;
            }
        }

        private void UpdateUsernameConfig(string newUsername)
        {
            if (Model.UserId == _userSessionService.CurrentUser?.UserId)
            {
                _registryService.GetUserLoginConfig(out string? username, out string? password);
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    _registryService.SetUserLoginConfig(newUsername, password);
                }
            }
        }

        protected override bool ValidateModel()
        {
            if (Status.SelectMode == AddEditStatus.EnMode.Edit)
                Model.Password = _hashService.HashPassword("FakePassword");

            if (!base.ValidateModel()) return false;
            if (!ValidatePersonSelection()) return false;
            if (!ValidateBranchSelection()) return false;

            return true;
        }

        private async Task<bool> ValidateUsername()
        {
            var usernameValidator = new UserValidation(_service);
            var result = await usernameValidator.ValidateFullUsernameAsync(Model.UserId, Model.Username);

            if (result != ValidationResult.Success)
            {
                ShowValidationError("التحقق من اسم المستخدم", result!.ErrorMessage!);
                return false;
            }

            return true;
        }

        protected override void SendMessage()
        {
            base.SendMessage();
            RefreshUserSession();
        }

        private async void InitializeBranches()
        {
            var branchOption = await _branchService.GetAllBranchesOption();
            Branches = new ObservableCollection<BranchOptionModel>(branchOption!);
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