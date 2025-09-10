using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class BranchDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Branch Name is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Branch Name must be between 5 and 20 characters")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Branch Address is required")]
    [StringLength(100, MinimumLength = 15, ErrorMessage = "Branch Address must be between 15 and 100 characters")]
    public string Address { get; set; } = null!;
}
