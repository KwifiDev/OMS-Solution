using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using OMS.UI.APIs.Services.Interfaces.Views;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace OMS.UI.ViewModels.Pages
{
    public partial class DashboardPageViewModel : ObservableObject
    {
        public record DashboardCard(PackIconKind Icon, Brush Color, string Title, decimal? Value);
        private readonly IDashboardSummaryService _dashboardSummaryService;

        [ObservableProperty]
        private ObservableCollection<DashboardCard>? _cards;

        public DashboardPageViewModel(IDashboardSummaryService dashboardSummaryService)
        {
            _dashboardSummaryService = dashboardSummaryService;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var dashboardSummary = (await _dashboardSummaryService.GetAllAsync()).FirstOrDefault();
            if (dashboardSummary is null) return;

            Cards =
            [
                new(PackIconKind.CurrencyUsd, Brushes.LawnGreen, "جميع العائدات", dashboardSummary.TotalRevenues),
                new(PackIconKind.AccountArrowUp, Brushes.LightSeaGreen, "الايداعات", dashboardSummary.Deposit),
                new(PackIconKind.AccountArrowDown, Brushes.LightSalmon, "السحوبات", dashboardSummary.Withdraw),
                new(PackIconKind.Package, Brushes.LightSkyBlue, "ارصدة الزبائن", dashboardSummary.TotalBalance),
                new(PackIconKind.AccountBalanceWallet,Brushes.LightYellow, "دفعات الزبائن", dashboardSummary.TotalPayments),
                new(PackIconKind.SaleCircle, Brushes.LightSteelBlue, "جميع المبيعات", dashboardSummary.TotalSales),
                new(PackIconKind.Discount, Brushes.LightCoral, "الخصومات على المبيعات", dashboardSummary.TotalSalesAmountDeducted),
                new(PackIconKind.MoneyOff ,Brushes.LightBlue, "الديون الغير مدفوعة", dashboardSummary.NotPaidDebts),
                new(PackIconKind.Cash, Brushes.LightCyan, "الديون المدفوعة", dashboardSummary.PaidDebts),
                new(PackIconKind.DiscountCircle, Brushes.Peru, "الخصومات على الديون", dashboardSummary.TotalDebtsAmountDeducted),
                new(PackIconKind.Monitor, Brushes.LightGoldenrodYellow, "التدفق المالي", dashboardSummary.NetCashFlow),
                new(PackIconKind.ClipboardFlow, Brushes.LimeGreen, "جميع المبالغ المدخلة", dashboardSummary.TotalIncome)
            ];

        }


    }
}
