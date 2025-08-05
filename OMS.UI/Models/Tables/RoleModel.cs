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

        [Required, MinLength(3, ErrorMessage = "الدور على الاقل مكون 3 محارف")]
        [RegularExpression(@"^[a-zA-z]+$", ErrorMessage = "ضع احرف انكليزية فقط")]
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string RoleIdDisplay => _id > 0 ? _id.ToString() : "لا يوجد";
    }
}
