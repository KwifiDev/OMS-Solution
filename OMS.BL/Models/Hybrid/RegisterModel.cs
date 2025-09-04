using OMS.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Hybrid
{
    public class RegisterModel : IModelKey
    {
        [Key]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int BranchId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; } = false;
    }
}
