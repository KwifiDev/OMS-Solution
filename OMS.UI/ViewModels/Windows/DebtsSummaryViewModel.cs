using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OMS.Common.Data;
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Loading;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.ViewModels.Pages;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class DebtsSummaryViewModel : BasePageViewModel<IDebtService, IDebtsSummaryService, DebtsSummaryModel, DebtModel>, IDialogInitializer<(int ClientId, int AccountId)>
    {
        protected override string ViewClaim => PermissionsData.DebtsSummary.View;
        protected override string AddClaim => PermissionsData.Debts.Add;
        protected override string EditClaim => PermissionsData.Debts.Edit;
        protected override string DeleteClaim => PermissionsData.Debts.Delete;


        private readonly IWindowService _windowService;
        private readonly IUserAccountService _userAccountService;
        private int _clientId;
        private int _accountId;

        [ObservableProperty]
        private UserAccountModel _userAccount = null!;

        [ObservableProperty]
        private PaginationInfo _paginationInfo = new();

        [ObservableProperty]
        private decimal? _totalDebts = 0;

        public DebtsSummaryViewModel(IUserAccountService userAccountService, IDebtService service, ILoadingService loadingService, IDebtsSummaryService displayService,
                                     IDialogService dialogService, IMessageService messageService, IWindowService windowService, IUserSessionService userSessionService)
                                     : base(service, displayService, loadingService, dialogService, messageService, userSessionService)
        {
            _userAccountService = userAccountService;
            _windowService = windowService;

            SelectedItemChanged += OnSelectedItemChanged;

            CommandConditions[nameof(EditItemCommand)] += CanOpenPayDebtDialog;
            CommandConditions[nameof(DeleteItemCommand)] += CanOpenPayDebtDialog;

            PaginationInfo.PageChanged += OnPageChanged;

            WeakReferenceMessenger.Default.Register<PayDebtModel>(this, OnPayDebtReceived);
        }

        private async Task OnPageChanged()
        {
            await LoadDebtsData();
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
            var pagedResultDebtsData = await _displayService.GetDebtsByClientIdPagedAsync(_clientId, new PaginationParams(PaginationInfo.CurrentPage, PaginationInfo.PageSize));
            if (pagedResultDebtsData is null) return false;

            Items = new(pagedResultDebtsData.Items);
            PaginationInfo.CurrentPage = pagedResultDebtsData.PageNumber;
            PaginationInfo.PageSize = pagedResultDebtsData.PageSize;
            PaginationInfo.TotalItems = pagedResultDebtsData.TotalItems;
            PaginationInfo.TotalPages = pagedResultDebtsData.TotalPages;

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



        [RelayCommand(CanExecute = nameof(CanCancelDebt))]
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

        private bool CanCancelDebt()
        {
            return SelectedItem?.Status == "غير مدفوع" && _userSessionService.Claims!.Contains(PermissionsData.Debts.Cancel);
        }


        [RelayCommand]
        private void Close() => _windowService.Close();

        [RelayCommand(CanExecute = nameof(CanOpenPayDebtDialog))]
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

        private bool CanOpenPayDebtDialog()
        {
            return SelectedItem?.Status == "غير مدفوع" && SelectedItem.TotalDebts <= UserAccount?.ClientBalance
                 && _userSessionService.Claims!.Contains(PermissionsData.Debts.Pay);
        }


        [RelayCommand(CanExecute = nameof(CanPayAllDebts))]
        private async Task PayAllClientDebts()
        {
            bool isOpened = await _dialogService.ShowDialog<PayDebtWindow, (int Id, DebtStatus.EnMode)>(
                (_clientId, DebtStatus.EnMode.PayAllDebtsByClientId));

            if (isOpened)
                await LoadData();
        }

        private bool CanPayAllDebts()
        {
            return TotalDebts <= UserAccount?.ClientBalance && TotalDebts != 0 && _userSessionService.Claims!.Contains(PermissionsData.Debts.Pay);
        }


        [RelayCommand(CanExecute = nameof(CanOpenClientPaymentsDialog))]
        private async Task OpenClientPaymentsDialog()
        {
            await _dialogService.ShowDialog<AccountPaymentsWindow, int>(_accountId);
        }

        private bool CanOpenClientPaymentsDialog()
        {
            return _userSessionService.Claims!.Contains(PermissionsData.PaymentsSummary.View);
        }

        [RelayCommand(CanExecute = nameof(CanShowAccountTransactions))]
        private async Task ShowAccountTransactions(int accountId)
        {
            await _dialogService.ShowDialog<AccountTransactionsWindow, int>(accountId);
        }

        private bool CanShowAccountTransactions()
        {
            return _userSessionService.Claims!.Contains(PermissionsData.TransactionsSummary.View);
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
