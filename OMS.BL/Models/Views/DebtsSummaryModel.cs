using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class DebtsSummaryModel : IModelKey
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public decimal? TotalDebts { get; set; }

    public EnDebtStatus Status { get; set; }

    public DateOnly CreatedAt { get; set; }
}
