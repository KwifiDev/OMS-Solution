namespace OMS.UI.APIServices.Dtos.Views;

public partial class DebtsSummaryDto
{
    public int DebtId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? TotalDebts { get; set; }

    public string Status { get; set; } = null!;
}
