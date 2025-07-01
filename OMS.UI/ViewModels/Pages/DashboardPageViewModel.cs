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

        private readonly Stack<Brush> _cardColorStack = new();
        private static readonly Brush[] PastelBrushes =
        {
            Brushes.Lavender, Brushes.LightBlue, Brushes.LightCyan,
            Brushes.LightGoldenrodYellow, Brushes.LightGreen,
            Brushes.LightPink, Brushes.LightSalmon, Brushes.LightSkyBlue,
            Brushes.LightSteelBlue, Brushes.LightYellow,
            Brushes.MintCream, Brushes.PaleGoldenrod,
            Brushes.PaleGreen, Brushes.PaleTurquoise,
            Brushes.PowderBlue, Brushes.Thistle
        };

        public DashboardPageViewModel(IDashboardSummaryService dashboardSummaryService)
        {
            _dashboardSummaryService = dashboardSummaryService;
        }

        private void InitializeColorStack()
        {
            var random = new Random();
            var randomColors = PastelBrushes.OrderBy(_ => random.Next()).Take(12);

            foreach (var color in randomColors)
            {
                _cardColorStack.Push(color);
            }
        }

        [RelayCommand]
        private async Task LoadData()
        {
            var dashboardSummary = await _dashboardSummaryService.GetData();

            if (dashboardSummary is null)
                return;

            InitializeColorStack();

            Cards =
            [
                CreateCard(PackIconKind.CurrencyUsd, "جميع العائدات", dashboardSummary.TotalRevenues),
                CreateCard(PackIconKind.AccountArrowUp, "الايداعات", dashboardSummary.Deposit),
                CreateCard(PackIconKind.AccountArrowDown, "السحوبات", dashboardSummary.Withdraw),
                CreateCard(PackIconKind.Package, "ارصدة الزبائن", dashboardSummary.TotalBalance),
                CreateCard(PackIconKind.AccountBalanceWallet, "دفعات الزبائن", dashboardSummary.TotalPayments),
                CreateCard(PackIconKind.SaleCircle, "جميع المبيعات", dashboardSummary.TotalSales),
                CreateCard(PackIconKind.Discount, "الخصومات على المبيعات", dashboardSummary.TotalSalesAmountDeducted),
                CreateCard(PackIconKind.MoneyOff, "الديون الغير مدفوعة", dashboardSummary.NotPaidDebts),
                CreateCard(PackIconKind.Cash, "الديون المدفوعة", dashboardSummary.PaidDebts),
                CreateCard(PackIconKind.DiscountCircle, "الخصومات على الديون", dashboardSummary.TotalDebtsAmountDeducted),
                CreateCard(PackIconKind.Monitor, "التدفق المالي", dashboardSummary.NetCashFlow),
                CreateCard(PackIconKind.ClipboardFlow, "جميع المبالغ المدخلة", dashboardSummary.TotalIncome)
            ];
        }

        private DashboardCard CreateCard(PackIconKind icon, string title, decimal? value)
        {
            return new DashboardCard(icon, _cardColorStack.Pop(), title, value);
        }
    }
}