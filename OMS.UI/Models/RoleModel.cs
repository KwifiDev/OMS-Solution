namespace OMS.UI.Models
{
    public class RoleModel : BaseModel
    {
        private int _id;
        private string? _name;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
