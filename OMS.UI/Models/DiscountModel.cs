using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class DiscountModel : BaseModel
    {
        private int _discountId;
        private int _serviceId;
        private EnClientType _clientType;
        private decimal _discountPercentage = 1;


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

        [Required(ErrorMessage = "نوع العميل مطلوب")]
        [Range(0, 2, ErrorMessage = "|يجب ان يكون نوع العميل |محامي - عادي - اخر")]
        public EnClientType ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }

        [Required(ErrorMessage = "يجب تحدد نسبة الخصم")]
        [Range(1, 99, ErrorMessage = "DiscountPercentage must be Between [1 - 99]")]
        public decimal DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        public string DiscountIdDisplay => _discountId > 0 ? _discountId.ToString() : "لا يوجد";
    }
}
