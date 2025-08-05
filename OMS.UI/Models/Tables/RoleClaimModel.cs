using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class RoleClaimModel : BaseModel
    {
        private int _id;
        private int _roleId;
        private string? _claimType;
        private string? _claimValue;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }

        [Required(ErrorMessage = "حدد نوع المطالبة")]
        public string? ClaimType
        {
            get => _claimType;
            set => SetProperty(ref _claimType, value);
        }

        [Required(ErrorMessage = "ضع قيمة المطالبة")]
        [RegularExpression(@"^[a-zA-Z]+\.[a-zA-Z]+$", ErrorMessage = "الادخال يجب ان يكون مثل 'Exp.Add'")]
        public string? ClaimValue
        {
            get => _claimValue;
            set => SetProperty(ref _claimValue, value);
        }
    }
}