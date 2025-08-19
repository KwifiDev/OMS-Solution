using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.Common.Enums;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Records;
using OMS.UI.Models.Views;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountTransactionViewModel : ObservableObject, IDialogInitializer<TransactionParams>
    {
        private readonly IAccountService _accountService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;


        [ObservableProperty]
        private AccountTransactionModel _accountTransaction = null!;

        [ObservableProperty]
        private UserAccountModel _userAccount = null!;

        [ObservableProperty]
        private TransactionStatus _transactionStatus;

        public ClientAccountTransactionViewModel(IAccountService accountService, IUserAccountService userAccountService,
                                                 IUserSessionService userSessionService, IMessageService messageService,
                                                 IWindowService windowService, IStatusService statusService, IDialogService dialogService)
        {
            _accountService = accountService;
            _userAccountService = userAccountService;
            _userSessionService = userSessionService;
            _messageService = messageService;
            _windowService = windowService;
            _dialogService = dialogService;

            TransactionStatus = statusService.CreateTransactionStatus();
        }



        public virtual async Task<bool> OnOpeningDialog(TransactionParams? transaction)
        {
            if (transaction == null) return false;
            TransactionStatus.SelectMode = transaction.Mode;

            try
            {
                bool isSuccess = false;

                if (transaction.AccountId > 0 && (isSuccess = await LoadClientAccountAsync(transaction.AccountId)))
                {
                    if (isSuccess)
                    {
                        AccountTransaction = new AccountTransactionModel
                        {
                            AccountId = (int)transaction.AccountId,
                            CreatedByUserId = _userSessionService.CurrentUser!.UserId,
                            TransactionType = (EnTransactionType)transaction.Mode
                        };
                    }
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                ShowInitializationError(ex);
                return false;
            }
        }

        private async Task<bool> LoadClientAccountAsync(int? accountId)
        {
            if (accountId == null)
            {
                ShowError();
                return false;
            }

            var userAccountModel = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountModel == null)
            {
                ShowSearchError();
                return false;
            }

            UserAccount = userAccountModel;

            return true;
        }


        [RelayCommand(CanExecute = nameof(CanStartTansaction))]
        private async Task StartTansaction(object? parameter)
        {
            if (!_messageService.ShowQuestionMessage("اجراء مناقلة",
                TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit ?
                MessageTemplates.DepositConfirmation : MessageTemplates.WithdrawalConfirmation)) return;

            if (!ValidateModel()) return;

            bool isSuccess = await SaveDataAsync(AccountTransaction);

            if (!isSuccess)
            {
                ShowTransactionError();
                AccountTransaction.TransactionStatus = EnAccountTransactionStatus.Empty;
                return;
            }

            UpdateStatusAndNotify();

            await LoadClientAccountAsync(AccountTransaction.AccountId);
        }

        private bool CanStartTansaction()
        {
            return _userSessionService.Claims!.Contains(PermissionsData.Accounts.Transaction);
        }

        private bool ValidateModel()
        {
            if (!AccountTransaction.ArePropertiesValid())
            {
                ShowValidationError(AccountTransaction.GetErrors()?.FirstOrDefault()?.ErrorMessage);
                return false;
            }
            return true;
        }

        protected virtual void UpdateStatusAndNotify()
        {
            NotifyTransactionSuccess();
            ShowSuccessMessage();
            SaveModelOnStatus();
        }

        private void NotifyTransactionSuccess()
        {
            switch (TransactionStatus.SelectMode)
            {
                case TransactionStatus.EnMode.Deposit:
                    TransactionStatus.Operation = TransactionStatus.EnExecuteOperation.Deposited;
                    break;

                case TransactionStatus.EnMode.Withdraw:
                    TransactionStatus.Operation = TransactionStatus.EnExecuteOperation.Withdrawn;
                    break;

                case TransactionStatus.EnMode.Transfer:
                    TransactionStatus.Operation = TransactionStatus.EnExecuteOperation.Transferred;
                    break;
            }
        }

        private void SaveModelOnStatus() =>
            TransactionStatus.ModelObject = AccountTransaction;


        [RelayCommand(CanExecute = nameof(CanShowAccountTransactions))]
        private async Task ShowAccountTransactions(int accountId)
        {
            await _dialogService.ShowDialog<AccountTransactionsWindow, int>(accountId);
        }

        private bool CanShowAccountTransactions()
        {
            return _userSessionService.Claims!.Contains(PermissionsData.TransactionsSummary.View);
        }

        [RelayCommand]
        private void Close() => _windowService.Close();

        private async Task<bool> SaveDataAsync(AccountTransactionModel model)
        {
            return await _accountService.StartTransactionAsync(model);
        }


        #region Common Message Handlers
        // Common message handling methods
        private void ShowSuccessMessage()
        {
            bool isDeposit = TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit;
            var messageType = isDeposit ? MessageTemplates.AdditionSuccessMessage : MessageTemplates.UpdateSuccessMessage;

            _messageService.ShowInfoMessage($"إجراء {(isDeposit ? "إيداع" : "سحب")} {"حساب العميل"}", messageType);
        }

        private void ShowInitializationError(Exception ex) =>
            _messageService.ShowErrorMessage("خطأ في التهيئة", ex.Message);

        private void ShowValidationError(string? error) =>
            _messageService.ShowInfoMessage("تحقق", MessageTemplates.ValidationErrorMessage(error));

        private void ShowSearchError() =>
            _messageService.ShowErrorMessage("اجراء البحث عن النموذج", MessageTemplates.SearchErrorMessage);

        private void ShowError() =>
            _messageService.ShowErrorMessage("خطأ", MessageTemplates.ErrorMessage);

        private void ShowTransactionError()
        {
            switch (AccountTransaction.TransactionStatus)
            {
                case EnAccountTransactionStatus.Failed:

                    if (TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit)
                        _messageService.ShowErrorMessage("خطأ في عملية المناقلة",
                                                         TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit ?
                                                         MessageTemplates.DepositErrorMessage : MessageTemplates.WithdrawalErrorMessage);
                    break;

                case EnAccountTransactionStatus.InsufficientBalance:

                    _messageService.ShowErrorMessage("خطأ في عملية المناقلة", MessageTemplates.InsufficientBalanceErrorMessage);

                    break;
            }
        }
        #endregion

    }
}