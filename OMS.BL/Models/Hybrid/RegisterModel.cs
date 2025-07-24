using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Hybrid
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }

        public int PersonId { get; set; }

        public int BranchId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; } = false;
    }
}
