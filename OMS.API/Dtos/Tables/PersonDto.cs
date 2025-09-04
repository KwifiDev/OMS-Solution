using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class PersonDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MinLength(3, ErrorMessage = "First name must be at least 3 characters long.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Gender is required.")]
    [Range(0, 1, ErrorMessage = "Gender must be either 0 (Male) or 1 (Female).")]
    public EnGender Gender { get; set; }

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string? Phone { get; set; }

}
