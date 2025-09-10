using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class ChangePasswordDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "User Id Must Be Positive")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;


        [Required(ErrorMessage = "new Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

    }
}
