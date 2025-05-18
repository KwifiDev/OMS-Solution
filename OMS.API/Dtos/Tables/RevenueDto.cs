using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class RevenueDto
{
    [Key]
    public int RevenueId { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(1000, 1000000, ErrorMessage = "Amount Must Be Between [1,000 - 1,000,000]")]
    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
