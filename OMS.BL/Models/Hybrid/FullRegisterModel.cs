using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Hybrid
{
    public class FullRegisterModel
    {
        [Key]
        public int UserId { get; set; }

        public int PersonId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public EnGender Gender { get; set; }

        public string? Phone { get; set; }

        public int BranchId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; } = false;
    }
}
