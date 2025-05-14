namespace OMS.UI.APIs.Dtos.Views;

public partial class TransactionsByTypeDto
{
    public string TransactionType { get; set; } = null!;

    public int? TotalTransactions { get; set; }

    public string? TotalAmount { get; set; }
}
