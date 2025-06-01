using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Models.Records;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Pages
{
    public partial class ClientsPageViewModel : BasePageViewModel<IClientService, IClientsSummaryService, ClientsSummaryModel, ClientModel>
    {
        public ClientsPageViewModel(IClientService clientService, IClientsSummaryService clientsSummaryService,
                                    IDialogService dialogService, IMessageService messageService)
                                    : base(clientService, clientsSummaryService, dialogService, messageService)
        {
            SelectedItemChanged += NotifyCanExecuteChanged;
        }

        protected override async Task<bool> ExecuteDelete(int itemId)
        {
            return await _service.DeleteAsync(itemId);
        }

        protected override int GetItemId(ClientsSummaryModel item)
            => item.ClientId;

        protected override async Task LoadData()
        {
            var clientItems = await _displayService.GetAllAsync();
            Items = new(clientItems);
        }

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
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountDetails()
        {
            _dialogService.ShowDialog<ClientAccountDetailsWindow, int?>(SelectedItem?.AccountId);
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountDeposit()
        {
            _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Deposit));
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountWithdraw()
        {
            _dialogService.ShowDialog<ClientAccountTransactionWindow, TransactionParams>
                (new TransactionParams(SelectedItem?.AccountId, TransactionStatus.EnMode.Withdraw));
        }

        [RelayCommand(CanExecute = nameof(CanOpenAccountServices))]
        private void ShowClientAccountTransfer()
        {
            _messageService.ShowInfoMessage("لم يتم اجراء", "لم يتم انشاء هذه الأضافة بعد");
        }

        [RelayCommand]
        private void ShowSalesSummary()
        {
            _dialogService.ShowDialog<SalesSummaryWindow, int?>(SelectedItem?.ClientId);
        }

        private bool CanOpenAccountServices()
        {
            return SelectedItem?.AccountId != null;
        }

    }
}