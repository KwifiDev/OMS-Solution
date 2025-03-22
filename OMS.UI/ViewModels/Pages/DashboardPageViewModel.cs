using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace OMS.UI.ViewModels.Pages
{
    public class DashboardPageViewModel : ObservableObject
    {

        public record DashboardCard(PackIconKind Icon, string Title, string Value);

        public DashboardPageViewModel()
        {
            Cards = new ObservableCollection<DashboardCard>
            {
                new DashboardCard(PackIconKind.ChartLine, "المبيعات اليومية", "1,234,500 ل.س"),
                new DashboardCard(PackIconKind.People, "عدد الاشخاص", "1 شخص"),
                new DashboardCard(PackIconKind.AccountStar, "عدد العملاء", "1 عميل"),
                new DashboardCard(PackIconKind.Package, "الطلبات اليومية", "5 طلبات"),
                new DashboardCard(PackIconKind.CurrencyUsd, "إجمالي الأرباح", "2,500 ل.س"),
                new DashboardCard(PackIconKind.AlertCircle, "التنبيهات", "3 تنبيهات")
            };
        }

        public ObservableCollection<DashboardCard> Cards { get; }

    }
}
