using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;


public partial class ServiceDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Service Name is required")]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "Service Name must be between 5 and 25 characters")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Branch Name is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Branch Name must be between 5 and 100 characters")]
    public string Description { get; set; } = null!;

    [Range(500, double.MaxValue, ErrorMessage = "Price must be at least 500 l.s")]
    public decimal Price { get; set; }
}
