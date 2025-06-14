using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models
{
    public class SalesSummaryModel : ObservableObject
    {
        private int _saleId;
        private string _serviceName = null!;
        private string _description = null!;
        private string _notes = null!;
        private string? _totalSales;
        private string _status = null!;


        public int SaleId
        {
            get => _saleId;
            set => SetProperty(ref _saleId, value);
        }

        public string ServiceName
        {
            get => _serviceName;
            set => SetProperty(ref _serviceName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public string? TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}
