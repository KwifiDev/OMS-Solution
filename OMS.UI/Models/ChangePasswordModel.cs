using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class ChangePasswordModel : BaseModel
    {
        private int _userId;
        private string _oldPassword = null!;
        private string _newPassword = null!;


        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        [Required(ErrorMessage = "كلمة السر القديمة مطلوبة")]
        [MinLength(5, ErrorMessage = "كلمة السر لا تقل عن خمس محارف")]
        public string OldPassword
        {
            get => _oldPassword;
            set => SetProperty(ref _oldPassword, value);
        }


        [Required(ErrorMessage = "كلمة السر الجديدة مطلوبة")]
        [MinLength(5, ErrorMessage = "كلمة السر لا تقل عن خمس محارف")]
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }
    }
}
