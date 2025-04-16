﻿using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.Common.Enums;
using OMS.UI.Models;
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
    public partial class AddEditClientViewModel : AddEditBaseViewModel<ClientModel, ClientDto, IClientService>
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

        public AddEditClientViewModel(IClientService clientService, IAccountService accountService, IMapper mapper, IMessageService messageService,
                                      IFindPersonViewModel findPersonViewModel, IWindowService windowService, IStatusService statusService)
                                      : base(clientService, mapper, messageService, windowService, statusService)
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

        protected override async Task<ClientDto?> GetByIdAsync(int id)
            => await _service.GetByIdAsync(id);

        protected override async Task<bool> EnterEditModeAsync(int? id)
        {
            if (!await base.EnterEditModeAsync(id)) return false;

            LoadAssociatedPerson();
            await LoadAssociatedAccount();
            return true;
        }

        protected override async Task Save(object? parameter)
        {
            if (!ValidatePersonSelection()) return;

            var confirmation = ConfirmAccountDeletionIfNeeded();
            if (!confirmation) return;

            SetPersonReference();
            await base.Save(parameter);
        }

        protected override string GetEntityName() 
            => "عميل";

        protected override async Task<bool> SaveDataAsync(bool isAdding, ClientDto clientDto)
        {
            var clientSaved = await SaveClient(clientDto, isAdding);
            if (!clientSaved) return false;

            await HandleAccountOperations(clientDto.ClientId);
            return true;
        }

        protected override void UpdateModelAfterSave(ClientDto clientDto)
            => Model.ClientId = clientDto.ClientId;

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

        private async Task LoadAssociatedAccount()
        {
            var accountDto = await _accountService.GetByClientIdAsync(Model.ClientId);
            if (accountDto == null) return;

            ClientAccount = _mapper.Map<AccountModel>(accountDto);
            HasCreateUserAccount = true;
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
               ClientAccount.AccountId > 0 &&
               !HasCreateUserAccount;

        private void SetPersonReference()
            => Model.PersonId = FindPersonViewModel.Person!.PersonId;

        private async Task<bool> SaveClient(ClientDto clientDto, bool isAdding)
            => isAdding
                ? await _service.AddAsync(clientDto)
                : await _service.UpdateAsync(clientDto);

        private async Task HandleAccountOperations(int clientId)
        {
            if (HasCreateUserAccount)
            {
                await SaveOrUpdateAccount(clientId);
            }
            else
            {
                await RemoveExistingAccount();
            }
        }

        private async Task SaveOrUpdateAccount(int clientId)
        {
            ClientAccount.ClientId = clientId;
            var accountDto = _mapper.Map<AccountDto>(ClientAccount);

            var success = ClientAccount.AccountId == 0
                ? await _accountService.AddAsync(accountDto)
                : await _accountService.UpdateAsync(accountDto);

            if (!success) ShowAccountOperationError();
        }

        private async Task RemoveExistingAccount()
        {
            if (ClientAccount.AccountId <= 0) return;

            var success = await _accountService.DeleteAsync(ClientAccount.AccountId);
            if (!success) ShowAccountDeletionError();
        }

        private void ShowAccountOperationError()
            => _messageService.ShowInfoMessage("خطأ", MessageTemplates.ClientAccountAdditionError);

        private void ShowAccountDeletionError()
            => _messageService.ShowInfoMessage("خطأ", MessageTemplates.ClientAccountDeletionError);

        private void LoadAssociatedPerson()
        {
            FindPersonViewModel.PersonId = Model.PersonId.ToString();
            FindPersonViewModel.FindPerson();
        }

        private void ShowValidationError(string title, string message)
            => _messageService.ShowInfoMessage(title, message);
    }
}