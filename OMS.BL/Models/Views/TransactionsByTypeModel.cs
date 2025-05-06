namespace OMS.BL.Models.Views;

public partial class TransactionsByTypeModel
{
    public string TransactionType { get; set; } = null!;

    public int? TotalTransactions { get; set; }

    public string? TotalAmount { get; set; }
}
