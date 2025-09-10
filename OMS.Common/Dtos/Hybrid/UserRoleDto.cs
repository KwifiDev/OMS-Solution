using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class UserRoleDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "User Id Must Be Positive")]
        public int UserId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Role name must be at least 3 chars")]
        public string RoleName { get; set; } = null!;
    }
}
