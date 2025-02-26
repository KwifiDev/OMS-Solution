using CommunityToolkit.Mvvm.ComponentModel;

namespace OMS.UI.Models
{
    public partial class UserModel : ObservableValidator
    {
        private int _userId;
        private int _personId;
        private int _branchId;
        private string _userName = null!;
        private string _password = null!;
        private int _permissions;
        private bool _isActive;

        public int UserId
        {
            get => _userId;
            set
            {
                SetProperty(ref _userId, value);
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
        public string Username
        {
            get => _userName;
            set => SetProperty(ref _userName, value, validate: true);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, validate: true);
        }
        public int Permissions
        {
            get => _permissions;
            set => SetProperty(ref _permissions, value);
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
        public string UserIdDisplay => _userId > 0 ? _userId.ToString() : "لا يوجد";
        public string PersonIdDisplay => _personId > 0 ? _userId.ToString() : "لا يوجد";
        public string BranchIdDisplay => _branchId > 0 ? _userId.ToString() : "لا يوجد";
        public string IsActiveDisplay => IsActive ? "نشط" : "معطل";

        public bool ArePropertiesValid()
        {
            ValidateAllProperties();
            return !HasErrors;
        }
    }
}
