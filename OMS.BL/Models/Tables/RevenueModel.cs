using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class RevenueModel
{
    [Key]
    public int RevenueId { get; internal set; }

    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
