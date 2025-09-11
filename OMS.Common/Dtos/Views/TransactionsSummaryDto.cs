using OMS.Common.Enums;

namespace OMS.Common.Dtos.Views;

public partial class TransactionsSummaryDto
{
    public int Id { get; set; }

    public EnTransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string? Notes { get; set; }
}
