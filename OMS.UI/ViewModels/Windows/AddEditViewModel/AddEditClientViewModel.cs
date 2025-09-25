using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.UserControls.Interfaces;
using System.Collections.ObjectModel;
using static OMS.UI.ViewModels.UserControls.FindPersonViewModel;

namespace OMS.UI.ViewModels.Windows.AddEditViewModel
{
    public partial class AddEditClientViewModel : AddEditBaseViewModel<ClientModel, IClientService>
    {
        private readonly IAccountService _accountService;

        [ObservableProperty]
        private IFindPersonViewModel _findPersonViewModel;

        [ObservableProperty]
        private ObservableCollection<ClientType> _clientTypes = null!;

        [ObservableProperty]
        private bool _hasCreateUserAccount;

        [ObservableProperty]
        private AccountModel _clientAccount = new();

        public AddEditClientViewModel(IClientService clientService, IAccountService accountService, IMessageService messageService,
                                      IFindPersonViewModel findPersonViewModel, IWindowService windowService, IStatusService statusService)
                                      : base(clientService, messageService, windowService, statusService)
        {
            _accountService = accountService;
            _findPersonViewModel = findPersonViewModel;
            FindPersonViewModel.PersonFound += OnPersonFound;
            InitializeClientTypes();
        }

        public record ClientType(string DisplayMember, EnClientType Value);

        private async void OnPersonFound(object? obj, PersonFoundEventArgs e)
        {
            if (Status.SelectMode == AddEditStatus.EnMode.Edit) return;

            var clientId = await _service.GetIdByPersonIdAsync(e.PersonId);

            if (clientId == 0) return;

            await base.EnterEditModeAsync(clientId);
            await LoadAssociatedAccount();
        }

        protected override async Task<ClientModel?> GetByIdAsync(int id)
            => await _service.GetByIdAsync(id);

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            if (!await base.EnterEditModeAsync(id)) return false;

            var isPersonLoaded = await LoadAssociatedPerson();
            var isAccountLoaded = await LoadAssociatedAccount();
            return isPersonLoaded;
        }

        protected override async Task Save(object? parameter)
        {
            if (!ValidatePersonSelection()) return;

            if (HasCreateUserAccount && !ClientAccount.ArePropertiesValid()) return;

            var confirmation = ConfirmAccountDeletionIfNeeded();
            if (!confirmation) return;

            SetPersonReference();
            await base.Save(parameter);
        }

        protected override string GetEntityName()
            => "عميل";

        protected override async Task<bool> SaveDataAsync(bool isAdding, ClientModel clientModel)
        {
            var clientSaved = await SaveClient(clientModel, isAdding);
            if (!clientSaved) return false;

            return await HandleAccountOperations(clientModel.Id);
        }

        protected override bool ValidateModel()
        {
            if (!base.ValidateModel()) return false;
            if (FindPersonViewModel.Person != null) return true;

            ShowValidationError("تحقق", MessageTemplates.ValidationErrorMessage("يجب تحديد الشخص"));
            return false;
        }

        private void InitializeClientTypes()
        {
            ClientTypes =
            [
                new("عادي", EnClientType.Normal),
                new("محامي", EnClientType.Lawyer),
                new("اخر", EnClientType.Other)
            ];
        }

        private async Task<bool> LoadAssociatedAccount()
        {
            var accountModel = await _accountService.GetByClientIdAsync(Model.Id);
            if (accountModel == null)
            {
                return false;
            }

            ClientAccount = accountModel;
            HasCreateUserAccount = true;
            return true;
        }

        private bool ValidatePersonSelection()
        {
            if (FindPersonViewModel.Person != null) return true;

            ShowValidationError("شخص غير محدد", MessageTemplates.SaveErrorMessage);
            return false;
        }

        private bool ConfirmAccountDeletionIfNeeded()
        {
            if (!ShouldConfirmAccountDeletion()) return true;

            return _messageService.ShowQuestionMessage(
                "تنويه",
                "سوف يتم حذف حساب الرصيد الالكتروني\nهل انت متأكد من هذا الأجراء?"
            );
        }

        private bool ShouldConfirmAccountDeletion()
            => Status.SelectMode == AddEditStatus.EnMode.Edit &&
               ClientAccount.Id > 0 &&
               !HasCreateUserAccount;

        private void SetPersonReference()
            => Model.PersonId = FindPersonViewModel.Person!.Id;

        private async Task<bool> SaveClient(ClientModel clientModel, bool isAdding)
        {
            if (isAdding)
            {
                return HasCreateUserAccount
                    ? await _service.AddWithAccountAsync(new ClientAccountModel(clientModel, ClientAccount))
                    : await _service.AddAsync(clientModel);
            }
            else
            {
                return HasCreateUserAccount
                    ? await _service.UpdateWithAccountAsync(new ClientAccountModel(clientModel, ClientAccount))
                    : await _service.UpdateAsync(clientModel.Id, clientModel);
            }
        }


        private async Task<bool> HandleAccountOperations(int clientId)
        {
            if (!HasCreateUserAccount)
            {
                return await RemoveExistingAccount();
            }

            return true;
        }

        private async Task<bool> RemoveExistingAccount()
        {
            if (!IsClientHasAccount()) return true;

            var isRemoved = await _accountService.DeleteAsync(ClientAccount.Id);
            if (!isRemoved) ShowAccountDeletionError();

            return isRemoved;
        }

        private bool IsClientHasAccount() => ClientAccount.Id > 0;

        private void ShowAccountDeletionError()
            => _messageService.ShowInfoMessage("خطأ", MessageTemplates.ClientAccountDeletionError);

        private async Task<bool> LoadAssociatedPerson()
        {
            FindPersonViewModel.PersonId = Model.PersonId.ToString();
            await FindPersonViewModel.FindPerson();
            return FindPersonViewModel.Person != null;
        }

        private void ShowValidationError(string title, string message)
            => _messageService.ShowInfoMessage(title, message);
    }
}