using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class RevenueDto
{
    [Key]
    public int RevenueId { get; set; }

    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
