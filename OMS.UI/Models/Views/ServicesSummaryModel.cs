using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class ServicesSummaryModel : BaseModel
    {
        private int _serviceId;
        private string _name = null!;
        private decimal _price;
        private int? _totalconsumed;

        [Key]
        public int ServiceId
        {
            get => _serviceId;
            set => SetProperty(ref _serviceId, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public int? TotalConsumed
        {
            get => _totalconsumed;
            set => SetProperty(ref _totalconsumed, value);
        }
    }
}
