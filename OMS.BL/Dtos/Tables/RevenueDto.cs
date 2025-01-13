using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Dtos.Tables;

public partial class RevenueDto
{
    [Key]
    public int RevenueId { get; internal set; }

    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
