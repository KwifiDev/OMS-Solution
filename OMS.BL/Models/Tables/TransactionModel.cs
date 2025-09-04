using OMS.BL.Interfaces;
using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class TransactionModel : IModelKey
{
    [Key]
    public int Id { get; set; }

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
