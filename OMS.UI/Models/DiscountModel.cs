using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class DiscountModel : BaseModel
    {
        private int _discountId;
        private int _serviceId;
        private EnClientType _clientType;
        private decimal _discountPercentage;


        [Key]
        public int DiscountId
        {
            get => _discountId;
            set 
            { 
                SetProperty(ref _discountId, value); 
                OnPropertyChanged(nameof(DiscountIdDisplay));
            }
        }

        public int ServiceId
        {
            get => _serviceId;
            set => SetProperty(ref _serviceId, value);
        }

        [Required(ErrorMessage = "ClientType is Required")]
        [Range(0, 2, ErrorMessage = "ClientType Must Be Between [0 - 2]")]
        public EnClientType ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }

        [Required(ErrorMessage = "ClientType is Required")]
        [Range(1, 100, ErrorMessage = "DiscountPercentage must be Between [1 - 100]")]
        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        public string DiscountIdDisplay => _discountId > 0 ? _discountId.ToString() : "لا يوجد";
    }
}
