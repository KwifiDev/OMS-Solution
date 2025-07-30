namespace OMS.UI.Models.Others
{
    public class UserRoleSelectionModel : BaseModel
    {

        private int _roleId;
        private string _roleName = null!;
        private bool _isSelected;

        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }

        public string RoleName
        {
            get => _roleName;
            set => SetProperty(ref _roleName, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
