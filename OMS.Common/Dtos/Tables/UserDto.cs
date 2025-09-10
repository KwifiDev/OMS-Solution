using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class UserDto
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Person Id must be positive number")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "Branch ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Branch ID must be a positive number")]
    public int BranchId { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "UserName can only contain letters, numbers and underscores")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; } = false;
}