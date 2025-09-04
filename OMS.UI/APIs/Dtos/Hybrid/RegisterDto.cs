using OMS.Common.Enums;

namespace OMS.UI.APIs.Dtos.Hybrid
{
    namespace OMS.API.Dtos.Hybrid
    {
        public class RegisterDto
        {
            public int Id { get; set; }

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

}
