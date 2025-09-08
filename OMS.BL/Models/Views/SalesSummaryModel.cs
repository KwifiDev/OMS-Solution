using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class SalesSummaryModel : IModelKey
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public decimal? TotalSales { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }
}
