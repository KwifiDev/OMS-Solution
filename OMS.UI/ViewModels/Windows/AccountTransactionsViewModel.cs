using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models.Views;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AccountTransactionsViewModel : ObservableObject, IDialogInitializer<int>
    {
        private readonly ITransactionsSummaryService _transactionsSummaryService;
        private readonly IWindowService _windowService;

        public AccountTransactionsViewModel(ITransactionsSummaryService transactionsSummaryService, IWindowService windowService)
        {
            _transactionsSummaryService = transactionsSummaryService;
            _windowService = windowService;
        }


        [ObservableProperty]
        private ObservableCollection<TransactionsSummaryModel> _transactionsSummaryItems = new();


        public async Task<bool> OnOpeningDialog(int accountId)
        {
            TransactionsSummaryItems = new(await _transactionsSummaryService.GetTransactionsByAccountIdAsync(accountId));
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }
    }
}