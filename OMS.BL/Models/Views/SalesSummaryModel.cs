namespace OMS.BL.Models.Views;

public partial class SalesSummaryModel
{
    public int SaleId { get; set; }

    public int? ClientId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public string? TotalSales { get; set; }

    public string Status { get; set; } = null!;
}
