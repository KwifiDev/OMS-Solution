using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public partial class PersonModel : BaseModel
    {
        private int _personId;
        private string _firstName = null!;
        private string _lastName = null!;
        private EnGender _gender;
        private string? _phone;

        [Key]
        public int PersonId
        {
            get => _personId;
            set
            {
                SetProperty(ref _personId, value);
                OnPropertyChanged(nameof(PersonIdDisplay));
            }
        }

        [Required(ErrorMessage = "الاسم الاول مطلوب")]
        [MinLength(3, ErrorMessage = "الاسم على الاقل مكون من 3 احرف")]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, validate: true);
        }

        [Required(ErrorMessage = "النسب مطلوب")]
        [MinLength(3, ErrorMessage = "النسب على الاقل مكون من 3 احرف")]
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, validate: true);
        }

        public EnGender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        [Length(10, 10, ErrorMessage = "الهاتف يجب يتكون من 10 ارقام")]
        public string? Phone
        {
            get => string.IsNullOrWhiteSpace(_phone) ? null : _phone;
            set
            {
                SetProperty(ref _phone, value, validate: true);
                OnPropertyChanged(nameof(PhoneDisplay));
            }
        }

        // DisplayProperty
        public string GenderDisplay => _gender == EnGender.Male ? "ذكر" : "انثى";
        public string PhoneDisplay => _phone ?? "لا يوجد";
        public string PersonIdDisplay => _personId > 0 ? _personId.ToString() : "لا يوجد";
        public string FullName => $"{FirstName} {LastName}";
    }
}
