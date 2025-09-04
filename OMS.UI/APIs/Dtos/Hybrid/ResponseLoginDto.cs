using OMS.Common.Enums;

namespace OMS.UI.APIs.Dtos.Hybrid
{
    public class ResponseLoginDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int BranchId { get; set; }

        public string Username { get; set; } = null!;

        public int Permissions { get; set; }

        public bool IsActive { get; set; } = false;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public EnGender Gender { get; set; }
    }
}
