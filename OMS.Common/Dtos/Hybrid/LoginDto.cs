using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class LoginDto
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
