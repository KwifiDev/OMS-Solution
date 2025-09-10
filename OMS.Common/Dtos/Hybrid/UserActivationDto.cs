using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class UserActivationDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "User Id Must Be Positive")]
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
