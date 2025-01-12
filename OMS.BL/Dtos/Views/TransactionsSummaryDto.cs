namespace OMS.BL.Dtos.Views;

public partial class TransactionsSummaryDto
{
    public int TransactionId { get; set; }

    public string UserAccount { get; set; } = null!;

    public string TransactionType { get; set; } = null!;

    public string? Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Notes { get; set; } = null!;
}
