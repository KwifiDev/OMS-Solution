using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class UserDto
{
    [Key]
    public int UserId { get; set; }

    public int PersonId { get; set; }

    [Required(ErrorMessage = "Branch ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Branch ID must be a positive number")]
    public int BranchId { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers and underscores")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^[a-zA-Z0-9+/]{43}=$", ErrorMessage = "Password is not a valid SHA256 hash Base64.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public int Permissions { get; set; }

    public bool IsActive { get; set; } = false;
}