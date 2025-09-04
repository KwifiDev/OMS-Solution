using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class DiscountModel : BaseModel
    {
        private int _id;
        private int _serviceId;
        private EnClientType _clientType;
        private decimal _discountPercentage = 1;


        [Key]
        public int Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
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

        public string DiscountIdDisplay => _id > 0 ? _id.ToString() : "لا يوجد";
    }
}
