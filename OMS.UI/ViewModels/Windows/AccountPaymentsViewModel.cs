using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OMS.UI.APIs.Services.Interfaces.Views;
using OMS.UI.Models;
using OMS.UI.Services.Dialog;
using OMS.UI.Services.Windows;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Windows
{
    public partial class AccountPaymentsViewModel : ObservableObject, IDialogInitializer<int>
    {
        private readonly IPaymentsSummaryService _paymentsSummaryService;
        private readonly IWindowService _windowService;

        public AccountPaymentsViewModel(IPaymentsSummaryService paymentsSummaryService, IWindowService windowService)
        {
            _paymentsSummaryService = paymentsSummaryService;
            _windowService = windowService;
        }


        [ObservableProperty]
        private ObservableCollection<PaymentsSummaryModel> _paymentsSummaryItems = new();


        public async Task<bool> OnOpeningDialog(int accountId)
        {
            PaymentsSummaryItems = new(await _paymentsSummaryService.GetPaymentsByAccountIdAsync(accountId));
            return true;
        }

        [RelayCommand]
        private void Close()
        {
            _windowService.Close();
        }
    }
}