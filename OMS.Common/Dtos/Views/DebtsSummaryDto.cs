using OMS.Common.Enums;

namespace OMS.Common.Dtos.Views;

public partial class DebtsSummaryDto
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
