using CommunityToolkit.Mvvm.ComponentModel;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Validations;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Registry;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using static OMS.Common.Data.PermissionsData;
using static OMS.UI.ViewModels.UserControls.FindPersonViewModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditUserViewModel : AddEditBaseViewModel<UserModel, IUserService>, IDialogInitializer<(int? UserId, bool IsOpendOnUsersPage)>
    {
        private readonly IBranchService _branchService;
        private readonly IUserSessionService _userSessionService;
        private readonly IRegistryService _registryService;
        private readonly IAuthService _authService;


        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private ObservableCollection<BranchOptionModel> _branches = null!;


        private bool _isOpendByUsersPage = false;

        public AddEditUserViewModel(IUserService userService, IBranchService branchService, IMessageService messageService, IRegistryService registryService,
                                    IFindPersonViewModel findPersonViewModel, IWindowService windowService, IStatusService statusService,
                                    IUserSessionService userSessionService, IAuthService authService)
                                    : base(userService, messageService, windowService, statusService)
        {
            _branchService = branchService;
            _userSessionService = userSessionService;
            _findPersonViewModel = findPersonViewModel;
            _registryService = registryService;
            _authService = authService;

            FindPersonViewModel.PersonFound += OnPersonFound;

        }

        private async void OnPersonFound(object? obj, PersonFoundEventArgs e)
        {
            if (Status.SelectMode == AddEditStatus.EnMode.Edit) return;

            var userId = await _service.GetIdByPersonIdAsync(e.PersonId);
            if (userId == 0) return;

            await EnterEditModeAsync(userId);
        }

        //public override async Task<bool> OnOpeningDialog(int? id = -1)
        //{
        //    var isSuccess = await base.OnOpeningDialog(id);

        //    bool isBranchesLoaded = false;
        //    if (isSuccess) isBranchesLoaded = await InitializeBranches();

        //    return isSuccess && isBranchesLoaded;
        //}

        public async Task<bool> OnOpeningDialog((int? UserId, bool IsOpendOnUsersPage) parameters)
        {
            _isOpendByUsersPage = parameters.IsOpendOnUsersPage;

            var isSuccess = await base.OnOpeningDialog(parameters.UserId);

            bool isBranchesLoaded = false;
            if (isSuccess) isBranchesLoaded = await InitializeBranches();

            return isSuccess && isBranchesLoaded;
        }

        protected override async Task<UserModel?> GetByIdAsync(int id)
            => await _service.GetByIdAsync(id);

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            if (!await base.EnterEditModeAsync(id)) return false;

            var isPersonLoaded = await LoadAssociatedPerson();
            return isPersonLoaded;
        }

        protected override async Task Save(object? parameter)
        {
            if (!ValidatePersonSelection()) return;
            if (!ValidateModel()) return;
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
                return await _authService.CreateUserAsync(userModel);
            }
            else
            {
                bool isUpdated = IsCurrentUser() ? await _service.UpdateMyUserAsync(userModel.Id, userModel) : await _service.UpdateAsync(userModel.Id, userModel);
                if (isUpdated) UpdateUsernameConfig(userModel.Username);
                return isUpdated;
            }
        }

        private bool IsCurrentUser()
        {
            return _userSessionService.CurrentUser!.Id == Model.Id;
        }

        private void UpdateUsernameConfig(string newUsername)
        {
            if (Model.Id == _userSessionService.CurrentUser?.Id)
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
            if (Status.SelectMode == AddEditStatus.EnMode.Edit) Model.Password = "ChangePasswordDisabled";

            if (!base.ValidateModel()) return false;
            if (!ValidatePersonSelection()) return false;
            if (!ValidateBranchSelection()) return false;

            return true;
        }

        private async Task<bool> ValidateUsername()
        {
            var usernameValidator = new UserValidation(_service);
            var result = await usernameValidator.ValidateFullUsernameAsync(Model.Id, Model.Username);

            if (result != ValidationResult.Success)
            {
                ShowValidationError("التحقق من اسم المستخدم", result!.ErrorMessage!);
                return false;
            }

            return true;
        }

        protected override void SendMessage()
        {
            if (_isOpendByUsersPage)
                base.SendMessage();
            RefreshUserSession();
        }

        private async Task<bool> InitializeBranches()
        {
            var branchOption = await _branchService.GetAllBranchesOption();
            Branches = new ObservableCollection<BranchOptionModel>(branchOption!);

            return Branches.Count > 0;
        }

        private async Task<bool> LoadAssociatedPerson()
        {
            FindPersonViewModel.PersonId = Model.PersonId.ToString();
            await FindPersonViewModel.FindPerson();
            return FindPersonViewModel.Person != null;
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
            => Model.PersonId = FindPersonViewModel.Person!.Id;

        private void RefreshUserSession()
        {
            if (_userSessionService.CurrentUser?.PersonId == Model.PersonId)
                _userSessionService.UpdateModel();
        }

        private void ShowValidationError(string title, string message)
            => _messageService.ShowInfoMessage(title, message);


    }
}