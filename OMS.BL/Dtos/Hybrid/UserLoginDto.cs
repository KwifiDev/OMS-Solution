using OMS.Common.Enums;

namespace OMS.BL.Dtos.Hybrid
{
    public class UserLoginDto
    {
        public int UserId { get; internal set; }

        public int PersonId { get; set; }

        public int BranchId { get; internal set; }

        public string Username { get; set; } = null!;

        public int Permissions { get; set; }

        public bool IsActive { get; set; } = false;


        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public EnGender Gender { get; set; }
    }
}
