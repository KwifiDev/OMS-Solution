using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;

namespace OMS.UI.Models.Views
{
    public class DiscountsAppliedModel : ObservableObject
    {
        private int _id;
        private string _serviceName = null!;
        private decimal _servicePrice;
        private EnClientType _clientType;
        private decimal _discountPercentage;


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

        public EnClientType ClientType
        {
            get => _clientType;
            set 
            {
                SetProperty(ref _clientType, value); 
                OnPropertyChanged(nameof(ClientTypeDisplay));
            }
        }

        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        // Display Props
        public string ClientTypeDisplay => ClientType == EnClientType.Normal ? "عادي" :
                                           ClientType == EnClientType.Lawyer ? "محامي" : "أخرى";

        public string DiscountPercentageDisplay => $"{DiscountPercentage} %";
    }
}
