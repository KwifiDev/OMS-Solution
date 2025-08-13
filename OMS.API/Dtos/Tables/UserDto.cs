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

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "UserName can only contain letters, numbers and underscores")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; } = false;
}