namespace OMS.BL.Models.Views;

public partial class SalesSummaryModel
{
    public int SaleId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? TotalSales { get; set; }

    public string Status { get; set; } = null!;
}
