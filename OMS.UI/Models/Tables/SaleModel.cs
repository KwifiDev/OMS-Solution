using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class SaleModel : BaseModel
    {
        private int _id;
        private int _clientId;
        private int _serviceId;
        private decimal _cost;
        private short _quantity = 1;
        private decimal? _discountPercentage;
        private decimal? _amountDeducted;
        private decimal? _total;
        private string? _description;
        private string? _notes;
        private DateOnly _createdAt;
        private EnSaleStatus _status;
        private int _createdByUserId;


        [Key]
        public int Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
                OnPropertyChanged(nameof(SaleIdDisplay));
            }
        }

        public int ClientId
        {
            get => _clientId;
            set => SetProperty(ref _clientId, value);
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "الرجاء تحديد نوع الخدمة")]
        public int ServiceId
        {
            get => _serviceId;
            set => SetProperty(ref _serviceId, value);
        }

        public decimal Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }

        public short Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public decimal? DiscountPercentage
        {
            get => _discountPercentage;
            set => SetProperty(ref _discountPercentage, value);
        }

        public decimal? AmountDeducted
        {
            get => _amountDeducted;
            set => SetProperty(ref _amountDeducted, value);
        }

        public decimal? Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        [MinLength(5, ErrorMessage = "الوصف على الاقل مكون من 5 احرف")]
        public string? Description
        {
            get => string.IsNullOrWhiteSpace(_description) ? null : _description;
            set => SetProperty(ref _description, value);
        }

        [MinLength(5, ErrorMessage = "الملاحظات على الاقل مكونة من 5 احرف")]
        public string? Notes
        {
            get => string.IsNullOrWhiteSpace(_notes) ? null : _notes;
            set => SetProperty(ref _notes, value);
        }

        public DateOnly CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        public EnSaleStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public int CreatedByUserId
        {
            get => _createdByUserId;
            set => SetProperty(ref _createdByUserId, value);
        }

        public string SaleIdDisplay => _id > 0 ? _id.ToString() : "لا يوجد";
    }
}
