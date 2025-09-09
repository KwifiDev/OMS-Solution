using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Hybrid
{
    public class ClaimDto
    {
        [Required(ErrorMessage = "Claim Type is required.")]
        [StringLength(100, ErrorMessage = "Claim Type cannot exceed 100 characters.")]
        public string ClaimType { get; set; } = null!;

        [Required(ErrorMessage = "Claim Value is required.")]
        [StringLength(100, ErrorMessage = "Claim Value cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z]+\.[a-zA-Z]+$", ErrorMessage = "Claim Value must be in format like 'Users.View' or 'Employees.Remove'.")]
        public string ClaimValue { get; set; } = null!;
    }
}
