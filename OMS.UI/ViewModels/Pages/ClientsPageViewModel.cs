using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Records;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.UserSession;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class ClientsPageViewModel : BasePageViewModel<IClientService, IClientsSummaryService, ClientsSummaryModel, ClientModel>
    {

        protected override string ViewClaim => PermissionsData.ClientsSummary.View;
        protected override string AddClaim => PermissionsData.Clients.Add;
        protected override string EditClaim => PermissionsData.Clients.Edit;
        protected override string DeleteClaim => PermissionsData.Clients.Delete;

        public ClientsPageViewModel(IClientService clientService, IClientsSummaryService clientsSummaryService,
                                    ILoadingService loadingService, IDialogService dialogService, IMessageService messageService, IUserSessionService userSessionService)
                                    : base(clientService, clientsSummaryService, loadingService, dialogService, messageService, userSessionService)
        {
            SelectedItemChanged += NotifyCanExecuteChanged;
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            return await _service.DeleteAsync(itemId);
        }

        protected override int GetItemId(ClientsSummaryModel item)
            => item.Id;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditClientWindow, int?>(itemId);

        protected override async Task<ClientsSummaryModel> ConvertToModel(ClientModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.Id))!;
        }

        private void NotifyCanExecuteChanged(object? obj, EventArgs e)
        {
            ShowClientAccountDetailsCommand.NotifyCanExecuteChanged();
            ShowClientAccountDepositCommand.NotifyCanExecuteChanged();
            ShowClientAccountWithdrawCommand.NotifyCanExecuteChanged();
            ShowClientAccountTransferCommand.NotifyCanExecuteChanged();
            ShowDebtsSummaryCommand.NotifyCanExecuteChanged();
            ShowSalesSummaryCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanShowClientAccountDetails))]
        private void ShowClientAccountDetails()
        {
            _dialogService.ShowDialog<ClientAccountDetailsWindow, int?>(SelectedItem?.AccountId);
        }

        private bool CanShowClientAccountDetails()
        {
            return SelectedItem?.AccountId != null && _userSessionService.Claims!.Contains(PermissionsData.Accounts.View);
        }

        [RelayCommand(CanExecute = nameof(CanShowClientAccountTransaction))]
        private async Task ShowClientAccountDeposit()
        {
            await _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Deposit));
        }

        [RelayCommand(CanExecute = nameof(CanShowClientAccountTransaction))]
        private async Task ShowClientAccountWithdraw()
        {
            await _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Withdraw));
        }

        [RelayCommand(CanExecute = nameof(CanShowClientAccountTransaction))]
        private void ShowClientAccountTransfer()
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
        }

        private bool CanShowClientAccountTransaction()
        {
            return SelectedItem?.AccountId != null && _userSessionService.Claims!.Contains(PermissionsData.Accounts.Transaction);
        }


        [RelayCommand(CanExecute = nameof(CanShowSalesSummary))]
        private async Task ShowSalesSummary()
        {
            await _dialogService.ShowDialog<SalesSummaryWindow, int?>(SelectedItem?.Id);
        }

        private bool CanShowSalesSummary()
        {
            return SelectedItem != null && _userSessionService.Claims!.Contains(PermissionsData.SalesSummary.View);
        }


        [RelayCommand(CanExecute = nameof(CanShowDebtsSummary))]
        private async Task ShowDebtsSummary()
        {
            await _dialogService.ShowDialog<DebtsSummaryWindow, (int ClientId, int AccountId)>((SelectedItem!.Id, (int)SelectedItem.AccountId!));
        }

        private bool CanShowDebtsSummary()
        {
            return SelectedItem?.AccountId != null && _userSessionService.Claims!.Contains(PermissionsData.DebtsSummary.View);
        }


    }
}