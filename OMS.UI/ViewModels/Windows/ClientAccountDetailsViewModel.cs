using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;
using OMS.UI.Views.Windows;

namespace OMS.UI.ViewModels.Windows
{
    public partial class ClientAccountDetailsViewModel : ObservableObject, IDialogInitializer<int?>
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IWindowService _windowService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private UserAccountModel _userAccount;

        public ClientAccountDetailsViewModel(IUserAccountService userAccountService, IWindowService windowService, IDialogService dialogService)
        {
            _userAccountService = userAccountService;
            _windowService = windowService;
            _dialogService = dialogService;
            _userAccount = new UserAccountModel();
        }


        public async Task<bool> OnOpeningDialog(int? accountId)
        {
            if (accountId == null) return false;

            var userAccountModel = await _userAccountService.GetByIdAsync((int)accountId);
            if (userAccountModel == null) return false;

            UserAccount = userAccountModel;
            return true;
        }


        [RelayCommand]
        private async Task ShowAccountTransactions(int accountId)
        {
            await _dialogService.ShowDialog<AccountTransactionsWindow, int>(accountId);
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }


    }
}