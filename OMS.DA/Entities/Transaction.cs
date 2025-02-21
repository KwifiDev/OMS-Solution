using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    /// <summary>
    /// 0 = Deposit | 1 = Withdraw | 2 = Transfer
    /// </summary>
    public EnTransactionType TransactionType { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int CreatedByUserId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Transactions")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("CreatedByUserId")]
    [InverseProperty("Transactions")]
    public virtual User CreatedByUser { get; set; } = null!;
}
