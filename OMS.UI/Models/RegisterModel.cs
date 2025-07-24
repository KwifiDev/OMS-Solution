using OMS.Common.Enums;

namespace OMS.UI.Models
{
    public class RegisterModel : BaseModel
    {
        private int _userId;
        private string _firstName = null!;
        private string _lastName = null!;
        private EnGender _gender;
        private string? _phone;
        private int _branchId;
        private string _userName = null!;
        private string _password = null!;
        private bool _isActive;

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public EnGender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public string? Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public int BranchId
        {
            get => _branchId;
            set => SetProperty(ref _branchId, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
    }
}
