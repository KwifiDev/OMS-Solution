namespace OMS.Common.Dtos.Views;

public partial class TransactionsSummaryDto
{
    public int Id { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Notes { get; set; } = null!;
}
