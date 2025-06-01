namespace OMS.UI.APIs.Dtos.Views;

public partial class SalesSummaryDto
{
    public int SaleId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? TotalSales { get; set; }

    public string Status { get; set; } = null!;
}
