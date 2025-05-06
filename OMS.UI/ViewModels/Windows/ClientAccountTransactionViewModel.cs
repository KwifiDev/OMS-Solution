using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.IServices.Tables;
using OMS.BL.IServices.Views;
using OMS.UI.Models;
using OMS.UI.Models.Records;
using OMS.UI.Resources.Strings;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.ShowMassage;
using OMS.UI.Services.StatusManagement;
using OMS.UI.Services.StatusManagement.Service;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountTransactionViewModel : ObservableObject, IDialogInitializer<TransactionParams>
    {
        private readonly IAccountService _accountService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private Models.AccountTransactionModel _accountTransaction = null!;

        [ObservableProperty]
        private UserAccountModel _userAccount = null!;

        [ObservableProperty]
        private TransactionStatus _transactionStatus;

        public ClientAccountTransactionViewModel(IAccountService accountService, IUserAccountService userAccountService,
                                                 IUserSessionService userSessionService, IMapper mapper, IMessageService messageService,
                                                 IWindowService windowService, IStatusService statusService)
        {
            _accountService = accountService;
            _userAccountService = userAccountService;
            _userSessionService = userSessionService;
            _mapper = mapper;
            _messageService = messageService;
            _windowService = windowService;

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
                        AccountTransaction = new Models.AccountTransactionModel
                        {
                            AccountId = (int)transaction.AccountId,
                            CreatedByUserId = _userSessionService.CurrentUser!.UserId
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

            var userAccountDto = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountDto == null)
            {
                ShowSearchError();
                return false;
            }

            UserAccount = _mapper.Map<UserAccountModel>(userAccountDto);

            return true;
        }


        [RelayCommand]
        private async Task StartTansaction(object? parameter)
        {
            if (!_messageService.ShowQuestionMessage("اجراء مناقلة",
                TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit ?
                MessageTemplates.DepositConfirmation : MessageTemplates.WithdrawalConfirmation)) return;

            if (!ValidateModel()) return;

            var dto = _mapper.Map<BL.Models.StoredProcedureParams.AccountTransactionModel>(AccountTransaction);

            bool isSuccess = await SaveDataAsync(dto);

            if (!isSuccess)
            {
                ShowTransactionError();
                return;
            }

            UpdateStatusAndNotify();

            await LoadClientAccountAsync(dto.AccountId);
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

        [RelayCommand]
        private void Close() => _windowService.Close();

        private async Task<bool> SaveDataAsync(BL.Models.StoredProcedureParams.AccountTransactionModel dto)
        {
            bool isSuccess = false;
            switch (TransactionStatus.SelectMode)
            {

                case TransactionStatus.EnMode.Deposit:
                    isSuccess = await _accountService.DepositIntoAccountAsync(dto);
                    AccountTransaction.TransactionStatus = dto.TransactionStatus;
                    break;

                case TransactionStatus.EnMode.Withdraw:
                    isSuccess = await _accountService.WithdrawFromAccountAsync(dto);
                    AccountTransaction.TransactionStatus = dto.TransactionStatus;
                    break;

                case TransactionStatus.EnMode.Transfer:
                    // not Implimented Yet
                    break;

            }

            return isSuccess;
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
                case Common.Enums.EnAccountTransactionStatus.Failed:

                    if (TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit)
                        _messageService.ShowErrorMessage("خطأ في عملية المناقلة",
                                                         TransactionStatus.SelectMode == TransactionStatus.EnMode.Deposit ?
                                                         MessageTemplates.DepositErrorMessage : MessageTemplates.WithdrawalErrorMessage);
                    break;

                case Common.Enums.EnAccountTransactionStatus.InsufficientBalance:

                    _messageService.ShowErrorMessage("خطأ في عملية المناقلة", MessageTemplates.InsufficientBalanceErrorMessage);

                    break;
            }
        }
        #endregion

    }
}