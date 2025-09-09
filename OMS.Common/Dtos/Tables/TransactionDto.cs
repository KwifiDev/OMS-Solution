using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class TransactionDto
{
    [Key]
    public int Id { get; set; }

    public int AccountId { get; set; }

    /// <summary>
    /// 0 = Deposit | 1 = Withdraw | 2 = Transfer
    /// </summary>
    public EnTransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int CreatedByUserId { get; set; }
}
