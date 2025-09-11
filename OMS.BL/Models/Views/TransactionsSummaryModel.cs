using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class TransactionsSummaryModel : IModelKey
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public EnTransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string? Notes { get; set; }
}
