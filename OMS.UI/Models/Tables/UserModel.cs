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

        [Required, Range(1, int.MaxValue, ErrorMessage = "يجب تحديد الفرع")]
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

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "يجب أن تكون كلمة المرور بين {2} و {1} حرفاً على الأقل.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
        ErrorMessage = "يجب أن تحتوي كلمة المرور على حرف كبير على الأقل، وحرف صغير على الأقل، ورقم على الأقل، وحرف خاص واحد على الأقل.")]
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
