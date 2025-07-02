using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class PersonalAppInfoModel : BaseModel
    {
        private string? _companyName;
        private string? _description;


        [MinLength(5, ErrorMessage = "اسم المكتبة على الاقل مكون من خمس احرف")]
        public string? CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        [MinLength(10, ErrorMessage = "وصف المكتبة على الاقل مكون من عشر احرف")]
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}
