using CommunityToolkit.Mvvm.Input;
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
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class ClientsPageViewModel : BasePageViewModel<IClientService, IClientsSummaryService, ClientsSummaryModel, ClientModel>
    {
        public ClientsPageViewModel(IClientService clientService, IClientsSummaryService clientsSummaryService, 
                                    ILoadingService loadingService, IDialogService dialogService, IMessageService messageService)
                                    : base(clientService, clientsSummaryService, loadingService, dialogService, messageService)
        {
            SelectedItemChanged += NotifyCanExecuteChanged;
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            return await _service.DeleteAsync(itemId);
        }

        protected override int GetItemId(ClientsSummaryModel item)
            => item.ClientId;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditClientWindow, int?>(itemId);

        protected override async Task<ClientsSummaryModel> ConvertToModel(ClientModel messageModel)
        {
            return (await _displayService.GetByIdAsync(messageModel.ClientId))!;
        }

        private void NotifyCanExecuteChanged(object? obj, EventArgs e)
        {
            ShowClientAccountDetailsCommand.NotifyCanExecuteChanged();
            ShowClientAccountDepositCommand.NotifyCanExecuteChanged();
            ShowClientAccountWithdrawCommand.NotifyCanExecuteChanged();
            ShowClientAccountTransferCommand.NotifyCanExecuteChanged();
            ShowDebtsSummaryCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountDetails()
        {
            _dialogService.ShowDialog<ClientAccountDetailsWindow, int?>(SelectedItem?.AccountId);
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private async Task ShowClientAccountDeposit()
        {
            await _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Deposit));
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private async Task ShowClientAccountWithdraw()
        {
            await _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Withdraw));
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountTransfer()
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
        }

        [RelayCommand]
        private async Task ShowSalesSummary()
        {
            await _dialogService.ShowDialog<SalesSummaryWindow, int?>(SelectedItem?.ClientId);
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private async Task ShowDebtsSummary()
        {
            await _dialogService.ShowDialog<DebtsSummaryWindow, (int ClientId, int AccountId)>((SelectedItem!.ClientId, (int)SelectedItem.AccountId!));
        }

        private bool CanOpenAccountServices()
        {
            return SelectedItem?.AccountId != null;
        }

    }
}