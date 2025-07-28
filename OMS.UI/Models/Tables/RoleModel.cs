using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class RoleModel : BaseModel
    {
        private int _id;
        private string? _name;

        [Key]
        public int Id
        {
            get => _id;
            set 
            {
                SetProperty(ref _id, value);
                OnPropertyChanged(nameof(RoleIdDisplay));
            } 
        }
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string RoleIdDisplay => _id > 0 ? _id.ToString() : "لا يوجد";
    }
}
