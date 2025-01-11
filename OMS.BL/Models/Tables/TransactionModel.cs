using OMS.DA.Enums;

namespace OMS.BL.Models.Tables;

public partial class TransactionModel
{
    public int TransactionId { get; internal set; }

    public int AccountId { get; internal set; }

    /// <summary>
    /// 0 = Deposit | 1 = Withdraw | 2 = Transfer
    /// </summary>
    public EnTransactionType TransactionType { get; internal set; }

    public decimal Amount { get; internal set; }

    public string? Notes { get; internal set; }

    public DateOnly CreatedAt { get; internal set; }

    public int CreatedByUserId { get; internal set; }

    // =======================================================
    public string TransactionTypeText
    {
        get => TransactionType == EnTransactionType.Deposit ? "ايداع" :
                TransactionType == EnTransactionType.Withdraw ? "سحب" :
                TransactionType == EnTransactionType.Transfer ? "تحويل" :
                "غير معرف";
    }
}
