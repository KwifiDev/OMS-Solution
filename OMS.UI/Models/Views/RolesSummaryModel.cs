namespace OMS.UI.Models.Views
{
    public class RolesSummaryModel : BaseModel
    {
        private int _roleId;
        private string? _roleName;
        private int? _usersCount;
        private int? _totalUsers;
        private double? _psercentageOfUsers;
        private int? _claimsCount;
        private string? _claimTypes;
        private string? _claimValues;


        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }

        public string? RoleName
        {
            get => _roleName;
            set => SetProperty(ref _roleName, value);
        }

        public int? UsersCount
        {
            get => _usersCount;
            set => SetProperty(ref _usersCount, value);
        }

        public int? TotalUsers
        {
            get => _totalUsers;
            set => SetProperty(ref _totalUsers, value);
        }

        public double? PercentageOfUsers
        {
            get => _psercentageOfUsers;
            set => SetProperty(ref _psercentageOfUsers, value);
        }

        public int? ClaimsCount
        {
            get => _claimsCount;
            set => SetProperty(ref _claimsCount, value);
        }

        public string? ClaimTypes
        {
            get => _claimTypes;
            set
            {
                SetProperty(ref _claimTypes, value);
                OnPropertyChanged(nameof(DisplayClaimTypes));
            }
        }

        public string? ClaimValues
        {
            get => _claimValues;
            set 
            {
                SetProperty(ref _claimValues, value);
                OnPropertyChanged(nameof(DisplayClaimValues));
            } 
        }

        // Display Props

        public string DisplayClaimTypes => _claimTypes ?? "لا يوجد";
        public string DisplayClaimValues => _claimValues ?? "لا يوجد";
    }
}
