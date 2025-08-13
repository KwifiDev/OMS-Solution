using OMS.Common.Enums;

namespace OMS.BL.Models.Hybrid
{
    public class UserLoginModel
    {
        public int UserId { get; internal set; }

        public int PersonId { get; set; }

        public int BranchId { get; internal set; }

        public string UserName { get; set; } = null!;

        public bool IsActive { get; set; } = false;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public EnGender Gender { get; set; }
    }
}
