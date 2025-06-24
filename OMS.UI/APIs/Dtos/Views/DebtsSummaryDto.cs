namespace OMS.UI.APIs.Dtos.Views;

public partial class DebtsSummaryDto
{
    public int DebtId { get; set; }

    public int? ClientId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public decimal? TotalDebts { get; set; }

    public string Status { get; set; } = null!;
}
