using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class TransactionDto
{
    [Key]
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
}
