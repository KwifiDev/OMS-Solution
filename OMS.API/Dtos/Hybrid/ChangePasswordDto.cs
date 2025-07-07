using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Hybrid
{
    public class ChangePasswordDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "Invalid user Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9+/]{43}=$", ErrorMessage = "Old Password is not a valid SHA256 hash Base64.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;


        [Required(ErrorMessage = "new Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9+/]{43}=$", ErrorMessage = "New Password is not a valid SHA256 hash Base64.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

    }
}
