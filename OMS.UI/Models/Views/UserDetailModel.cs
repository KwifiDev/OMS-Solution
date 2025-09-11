using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class UserDetailModel : ObservableObject
    {

        private int _id;
        private string _employeeName = null!;
        private string _username = null!;
        private bool _isActive;
        private string _workingBranch = null!;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
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

        public string WorkingBranch
        {
            get => _workingBranch;
            set => SetProperty(ref _workingBranch, value);
        }

        // Display Props
        public string IsActiveDisplay => IsActive ? "نشط" : "غير نشط";

    }
}
