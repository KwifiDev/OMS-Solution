using OMS.UI.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public partial class UserModel : BaseModel
    {
        private int _id;
        private int _personId;
        private int _branchId;
        private string _userName = null!;
        private string _password = null!;
        private bool _isActive;


        [Key]
        public int Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
                OnPropertyChanged(nameof(UserIdDisplay));
            }
        }
        public int PersonId
        {
            get => _personId;
            set
            {
                SetProperty(ref _personId, value);
                OnPropertyChanged(nameof(PersonIdDisplay));
            }
        }
        public int BranchId
        {
            get => _branchId;
            set
            {
                SetProperty(ref _branchId, value);
                OnPropertyChanged(nameof(BranchIdDisplay));
            }
        }

        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        [MinLength(3, ErrorMessage = "اسم المستخدم على الاقل من 3 احرف")]
        [CustomValidation(typeof(UserValidation), nameof(UserValidation.ValidateUsername))]
        public string Username
        {
            get => _userName;
            set => SetProperty(ref _userName, value, validate: true);
        }

        [Required(ErrorMessage = "نسيت تعيين كلمة السر")]
        [MinLength(5, ErrorMessage = "كلمة السر على الاقل مكون من 5 محارف")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, validate: true);
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                SetProperty(ref _isActive, value);
                OnPropertyChanged(nameof(IsActiveDisplay));
            }
        }

        // DisplayProperty
        public string UserIdDisplay => _id > 0 ? _id.ToString() : "لا يوجد";
        public string PersonIdDisplay => _personId > 0 ? _id.ToString() : "لا يوجد";
        public string BranchIdDisplay => _branchId > 0 ? _id.ToString() : "لا يوجد";
        public string IsActiveDisplay => IsActive ? "نشط" : "معطل";
    }
}
