using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class UserDetailModel : ObservableObject
    {

        private int _userId;
        private string _employeeName = null!;
        private string _username = null!;
        private string? _isActive;
        private string _workingBranch = null!;

        [Key]
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
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

        public string? IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public string WorkingBranch
        {
            get => _workingBranch;
            set => SetProperty(ref _workingBranch, value);
        }

    }
}
