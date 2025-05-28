using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class ServiceModel : BaseModel
    {
        private int _serviceId;
        private string _name = null!;
        private string _description = null!;
        private decimal _price;


        [Key]
        public int ServiceId
        {
            get => _serviceId;
            set 
            { 
                SetProperty(ref _serviceId, value);
                OnPropertyChanged(nameof(ServiceIdDisplay));
            }
        }

        [Required(ErrorMessage = "اسم الخدمة مطلوب")]
        [MinLength(3, ErrorMessage = "اسم الخدمة على الاقل مكون من 3 احرف")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [Required(ErrorMessage = "وصف الخدمة مطلوب")]
        [MinLength(10, ErrorMessage = "وصف الخدمة على الاقل مكون من 10 احرف")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [Required(ErrorMessage = "سعر الخدمة مطلوب")]
        [Range(typeof(decimal), "500", "1000000", ErrorMessage = "سعر الخدمة يجب ان يكون على الاقل 500")]
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        // DisplayProperty
        public string ServiceIdDisplay => _serviceId > 0 ? _serviceId.ToString() : "لا يوجد";
    }
}
