namespace OMS.Common.Dtos.Views;

public partial class SalesSummaryDto
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public decimal? TotalSales { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }
}
