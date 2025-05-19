namespace OMS.BL.Models.Views;

public partial class TransactionsSummaryModel
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Notes { get; set; } = null!;
}
