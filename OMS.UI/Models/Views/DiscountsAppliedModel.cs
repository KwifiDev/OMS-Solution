using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models.Views
{
    public class DiscountsAppliedModel : ObservableObject
    {
        private int _id;
        private string _serviceName = null!;
        private decimal _servicePrice;
        private string _clientType = null!;
        private string? _discount;


        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string ServiceName
        {
            get => _serviceName;
            set => SetProperty(ref _serviceName, value);
        }

        public decimal ServicePrice
        {
            get => _servicePrice;
            set => SetProperty(ref _servicePrice, value);
        }

        public string ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }

        public string? Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }
    }
}
