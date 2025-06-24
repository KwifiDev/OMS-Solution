using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
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

        public DebtsSummaryViewModel(IUserAccountService userAccountService, IDebtService service, IDebtsSummaryService displayService, IDialogService dialogService,
                                     IMessageService messageService, IWindowService windowService) : base(service, displayService, dialogService, messageService)
        {
            _userAccountService = userAccountService;
            SelectedItemChanged += OnSelectedItemChanged;
            CommandConditions[nameof(EditItemCommand)] += CanChangeDebt;
            CommandConditions[nameof(DeleteItemCommand)] += CanChangeDebt;
            _windowService = windowService;

            WeakReferenceMessenger.Default.Register<PayDebtModel>(this, (obj, payDebtModel) =>
            {
                if (payDebtModel.PayDebtStatus == EnPayDebtStatus.Success)
                {
                    if (SelectedItem is not null)
                    {
                        SelectedItem.Status = "مدفوع";
                        SelectedItem.Notes = payDebtModel.Notes is not null ? payDebtModel.Notes : "لا يوجد ملاحظات";
                    }
                }
            });
        }

        private void OnSelectedItemChanged(object? obj, EventArgs e)
        {
            CancelSaleCommand.NotifyCanExecuteChanged();
            ShowPayDebtCommand.NotifyCanExecuteChanged();
        }

        public async Task<bool> OnOpeningDialog((int ClientId, int AccountId) parameters)
        {
            if (parameters.ClientId <= 0 || parameters.AccountId <= 0) return false;

            _clientId = parameters.ClientId;
            _accountId = parameters.AccountId;

            bool isLoaded = await LoadData();

            return isLoaded;
        }

        protected override async Task<DebtsSummaryModel> ConvertToModel(DebtModel messageModel)
            => (await _displayService.GetByIdAsync(messageModel.DebtId))!;

        protected override async Task<bool> ExecuteDelete(int itemId) => await _service.DeleteAsync(itemId);

        protected override int GetItemId(DebtsSummaryModel item) => item.DebtId;

        protected override async Task<bool> LoadData()
        {
            if (!await LoadDebtsData()) return false;

            if (!await LoadUserAccount()) return false;

            return true;
        }

        private async Task<bool> LoadUserAccount()
        {
            var userAccount = await _userAccountService.GetByIdAsync(_accountId);
            if (userAccount is null) return false;

            UserAccount = userAccount;

            PayAllDebtsCommand.NotifyCanExecuteChanged();

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
            TotalDebts = Items.Where(debt => debt.Status == "غير مدفوع").Select(debt => debt.TotalDebts).Sum();
            PayAllDebtsCommand.NotifyCanExecuteChanged();
        }

        protected override Task ShowDetailsWindow(int itemId)
        {
            _messageService.ShowInfoMessage("معلومات", MessageTemplates.NotImplementedMessage);
            return Task.CompletedTask;
        }

        protected override async Task ShowEditorWindow(int? itemId = null)
            => await _dialogService.ShowDialog<AddEditDebtWindow, (int? DebtId, int ClientId)>((itemId, _clientId));

        [RelayCommand(CanExecute = nameof(CanChangeDebt))]
        private async Task CancelSale()
        {
            if (SelectedItem is null) return;

            if (!_messageService.ShowQuestionMessage("تحذير", MessageTemplates.CancellationDebtConfirmation))
                return;

            var isSuccess = await _service.CancelDebtAsync(SelectedItem.DebtId);

            if (!isSuccess)
            {
                _messageService.ShowErrorMessage("عملية الالغاء", MessageTemplates.CancelDebtErrorMessage);
                return;
            }

            SelectedItem.Status = "ملغات";
            _messageService.ShowInfoMessage("عملية الالغاء", MessageTemplates.CancelDebtSuccessMessage);
            CalcTotalDebts();
        }

        [RelayCommand]
        private void Close() => _windowService.Close();


        [RelayCommand(CanExecute = nameof(CanChangeDebt))]
        private async Task ShowPayDebt()
        {
            bool isOpend = await _dialogService.ShowDialog<PayDebtWindow, (int Id, DebtStatus.EnMode DebtOperation)>
                ((SelectedItem!.DebtId, DebtStatus.EnMode.PaySpecifiecDebt));

            if (isOpend)
            {
                CalcTotalDebts();
                await LoadUserAccount();
            }
        }

        private bool CanChangeDebt()
        {
            return SelectedItem?.Status == "غير مدفوع" && SelectedItem?.TotalDebts <= UserAccount?.ClientBalance;
        }

        private bool CanPayAllDebts()
        {
            return TotalDebts <= UserAccount?.ClientBalance && TotalDebts != 0;
        }


        [RelayCommand(CanExecute = nameof(CanPayAllDebts))]
        private async Task PayAllDebts()
        {
            bool isOpend = await _dialogService.ShowDialog<PayDebtWindow, (int Id, DebtStatus.EnMode DebtOperation)>
                ((_clientId, DebtStatus.EnMode.PayAllDebtsByClientId));

            if (isOpend) await LoadData();
        }


        [RelayCommand]
        private async Task ShowAccountTransactions(int accountId)
        {
            await _dialogService.ShowDialog<AccountTransactionsWindow, int>(accountId);
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
