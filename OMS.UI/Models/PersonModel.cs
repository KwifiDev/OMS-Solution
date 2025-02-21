using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public partial class PersonModel : ObservableValidator
    {
        private int _personId;
        private string _firstName = null!;
        private string _lastName = null!;
        private EnGender _gender;
        private string? _phone;

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
        [MinLength(3, ErrorMessage = "الاسم على الاقل مكون من ثلاث احرف")]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, validate: true);
        }

        [Required(ErrorMessage = "النسب مطلوب")]
        [MinLength(3, ErrorMessage = "النسب على الاقل مكون من ثلاث احرف")]
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

        public string? Phone
        {
            get => string.IsNullOrWhiteSpace(_phone) ? null : _phone;
            set
            {
                SetProperty(ref _phone, value);
                OnPropertyChanged(nameof(PhoneDisplay));
            }
        }

        // DisplayProperty
        public string PhoneDisplay => _phone ?? "لا يوجد";
        public string PersonIdDisplay => _personId > 0 ? _personId.ToString() : "لا يوجد";
        public string FullName => $"{FirstName} {LastName}";

        public bool ArePropertiesValid()
        {
            ValidateAllProperties();
            return !HasErrors;
        }
    }
}
