using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class DebtsSummaryViewModel : BasePageViewModel<IDebtService, IDebtsSummaryService, DebtsSummaryModel, DebtModel>, IDialogInitializer<(int ClientId, int AccountId)>
    {
        private readonly IWindowService _windowService;
        private readonly IUserAccountService _userAccountService;
        private int _clientId;
        private int _accountId;

        [ObservableProperty]
        private UserAccountModel _userAccount = null!;

        [ObservableProperty]
        private decimal? _totalDebts = 0;

        public DebtsSummaryViewModel(IUserAccountService userAccountService, IDebtService service, ILoadingService loadingService,
                                     IDebtsSummaryService displayService, IDialogService dialogService, IMessageService messageService, IWindowService windowService)
                                     : base(service, displayService, loadingService, dialogService, messageService)
        {
            _userAccountService = userAccountService;
            _windowService = windowService;

            SelectedItemChanged += OnSelectedItemChanged;

            CommandConditions[nameof(EditItemCommand)] += CanChangeDebt;
            CommandConditions[nameof(DeleteItemCommand)] += CanChangeDebt;

            WeakReferenceMessenger.Default.Register<PayDebtModel>(this, OnPayDebtReceived);
        }

        private void OnPayDebtReceived(object recipient, PayDebtModel payDebtModel)
        {
            if (payDebtModel.PayDebtStatus != EnPayDebtStatus.Success || SelectedItem is null) return;

            SelectedItem.Status = "مدفوع";
            SelectedItem.Notes = string.IsNullOrWhiteSpace(payDebtModel.Notes) ? "لا يوجد ملاحظات" : payDebtModel.Notes;
        }

        private void OnSelectedItemChanged(object? sender, EventArgs e)
        {
            CancelDebtCommand.NotifyCanExecuteChanged();
            OpenPayDebtDialogCommand.NotifyCanExecuteChanged();
        }

        public async Task<bool> OnOpeningDialog((int ClientId, int AccountId) parameters)
        {
            if (parameters.ClientId <= 0 || parameters.AccountId <= 0)
                return false;

            _clientId = parameters.ClientId;
            _accountId = parameters.AccountId;

            return await LoadData();
        }

        protected override async Task<bool> LoadData()
        {
            await LoadingService.ExecuteWithLoadingIndicator(async () =>
            {
                if (!await LoadDebtsData()) return;
                if (!await LoadUserAccount()) return;
            });

            //if (!await LoadDebtsData()) return false;
            //if (!await LoadUserAccount()) return false;

            return true;
        }

        private async Task<bool> LoadUserAccount()
        {
            var userAccount = await _userAccountService.GetByIdAsync(_accountId);
            if (userAccount is null) return false;

            UserAccount = userAccount;
            PayAllClientDebtsCommand.NotifyCanExecuteChanged();

            return true;
        }

        private async Task<bool> LoadDebtsData()
        {
            var debtsData = await _displayService.GetDebtsByClientIdAsync(_clientId);
            if (debtsData is null) return false;

            Items = new(debtsData);
            CalcTotalDebts();

            return true;
        }

        private void CalcTotalDebts()
        {
            TotalDebts = Items
                .Where(debt => debt.Status == "غير مدفوع")
                .Sum(debt => debt.TotalDebts);

            PayAllClientDebtsCommand.NotifyCanExecuteChanged();
        }

        private bool CanChangeDebt() =>
           SelectedItem?.Status == "غير مدفوع" &&
           SelectedItem.TotalDebts <= UserAccount?.ClientBalance;

        [RelayCommand(CanExecute = nameof(CanChangeDebt))]
        private async Task CancelDebt()
        {
            if (SelectedItem is null) return;

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.CancellationDebtConfirmation))
                return;

            var isSuccess = await _service.CancelDebtAsync(SelectedItem.DebtId);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("عملية الإلغاء", MessageTemplates.CancelDebtErrorMessage);
                return;
            }

            SelectedItem.Status = "ملغات";
            _messageService.ShowInfoMessage("عملية الإلغاء", MessageTemplates.CancelDebtSuccessMessage);
            CalcTotalDebts();
        }

        [RelayCommand]
        private void Close() => _windowService.Close();

        [RelayCommand(CanExecute = nameof(CanChangeDebt))]
        private async Task OpenPayDebtDialog()
        {
            bool isOpened = await _dialogService.ShowDialog<PayDebtWindow, (int Id, DebtStatus.EnMode)>(
                (SelectedItem!.DebtId, DebtStatus.EnMode.PaySpecifiecDebt));

            if (isOpened)
            {
                CalcTotalDebts();
                await LoadUserAccount();
            }
        }

        private bool CanPayAllDebts() =>
            TotalDebts <= UserAccount?.ClientBalance && TotalDebts != 0;

        [RelayCommand(CanExecute = nameof(CanPayAllDebts))]
        private async Task PayAllClientDebts()
        {
            bool isOpened = await _dialogService.ShowDialog<PayDebtWindow, (int Id, DebtStatus.EnMode)>(
                (_clientId, DebtStatus.EnMode.PayAllDebtsByClientId));

            if (isOpened)
                await LoadData();
        }

        [RelayCommand]
        private async Task OpenClientPaymentsDialog()
        {
            await _dialogService.ShowDialog<AccountPaymentsWindow, int>(_accountId);
        }

        [RelayCommand]
        private async Task ShowAccountTransactions(int accountId)
        {
            await _dialogService.ShowDialog<AccountTransactionsWindow, int>(accountId);
        }

        protected override async Task<DebtsSummaryModel> ConvertToModel(DebtModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.DebtId))!;

        protected override async Task<bool> ExecuteDelete(int itemId)
            => await _service.DeleteAsync(itemId);

        protected override int GetItemId(DebtsSummaryModel item)
            => item.DebtId;

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
        {
            await _dialogService.ShowDialog<AddEditDebtWindow, (int? DebtId, int ClientId)>((itemId, _clientId));
        }

        protected override async Task AddItem()
        {
            await base.AddItem();
            CalcTotalDebts();
        }

        protected override async Task DeleteItem()
        {
            await base.DeleteItem();
            CalcTotalDebts();
        }
    }
}
