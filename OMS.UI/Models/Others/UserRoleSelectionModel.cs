namespace OMS.UI.Models.Others
{
    public class UserRoleSelectionModel : BaseModel
    {

        private int _id;
        private string _roleName = null!;
        private bool _isSelected;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
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
