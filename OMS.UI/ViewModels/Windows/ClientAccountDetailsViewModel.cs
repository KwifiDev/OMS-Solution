using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.Common.Data;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.UserSession;
using OMS.UI.Services.Windows;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountDetailsViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;
        private readonly IUserSessionService _userSessionService;

        [ObservableProperty]
        private UserAccountModel _userAccount = new();

        public ClientAccountDetailsViewModel(IUserAccountService userAccountService, IWindowService windowService, IDialogService dialogService, IUserSessionService userSessionService)
        {
            _userAccountService = userAccountService;
            _windowService = windowService;
            _dialogService = dialogService;
            _userSessionService = userSessionService;
        }


        public async Task<bool> OnOpeningDialog(int? accountId)
        {
            if (accountId == null) return false;

            var userAccountModel = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountModel == null) return false;

            UserAccount = userAccountModel;
            return true;
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

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }


    }
}