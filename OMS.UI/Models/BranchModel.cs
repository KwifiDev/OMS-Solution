using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class BranchModel : BaseModel
    {
        private int _branchId;
        private string _name = null!;
        private string _address = null!;

        [Key]
        public int BranchId
        {
            get => _branchId;
            set
            {
                SetProperty(ref _branchId, value);
                OnPropertyChanged(nameof(BranchIdDisplay));
            }
        }

        [Required(ErrorMessage = "الاسم الفرع مطلوب")]
        [MinLength(5, ErrorMessage = "الاسم على الاقل مكون من خمس احرف")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, validate: true);
        }

        [Required(ErrorMessage = "عنوان الفرع مطلوب")]
        [MinLength(10, ErrorMessage = "العناون على الاقل مكون من عشر احرف")]
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value, validate: true);
        }

        // DisplayProperty
        public string BranchIdDisplay => _branchId > 0 ? _branchId.ToString() : "لا يوجد";
    }
}
