using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class InputUserRolesDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "User Id Must Be Positive")]
        public int UserId { get; set; }
        public ICollection<string> RolesToAdd { get; set; } = [];
        public ICollection<string> RolesToRemove { get; set; } = [];
    }
}
