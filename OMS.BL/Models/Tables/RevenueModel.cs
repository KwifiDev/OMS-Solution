namespace OMS.BL.Models.Tables;

public partial class RevenueModel
{
    public int RevenueId { get; internal set; }

    public required decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
