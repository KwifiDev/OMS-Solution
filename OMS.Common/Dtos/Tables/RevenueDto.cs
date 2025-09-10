using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class RevenueDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(1000, 1000000, ErrorMessage = "Amount Must Be Between [1,000 - 1,000,000]")]
    public required decimal Amount { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "Notes Length Must be Between (5 - 100)")]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
