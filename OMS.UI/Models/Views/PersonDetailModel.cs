using CommunityToolkit.Mvvm.ComponentModel;
using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class PersonDetailModel : ObservableObject
    {
        private int _id;
        private string _fullName = null!;
        private string _phone = null!;
        private EnGender _gender;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public EnGender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
    }
}
