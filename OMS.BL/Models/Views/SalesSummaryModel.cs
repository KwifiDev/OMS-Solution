using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class SalesSummaryModel : IModelKey
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? Notes { get; set; } = null!;

    public decimal? TotalSales { get; set; }

    public EnSaleStatus Status { get; set; }

    public DateOnly CreatedAt { get; set; }
}
