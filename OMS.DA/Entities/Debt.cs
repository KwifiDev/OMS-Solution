using OMS.DA.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Debt
{
    [Key]
    public int DebtId { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Cost { get; set; }

    public short Quantity { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal? AmountDeducted { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? Total { get; set; }

    [StringLength(100)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    /// <summary>
    /// 0 = NotPaid | 1 = Paid | 2 = Canceled
    /// </summary>
    public EnDebtStatus Status { get; set; }

    public int? PaymentId { get; set; }

    public int CreatedByUserId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Debts")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("CreatedByUserId")]
    [InverseProperty("Debts")]
    public virtual User CreatedByUser { get; set; } = null!;

    [ForeignKey("PaymentId")]
    [InverseProperty("Debts")]
    public virtual Payment? Payment { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("Debts")]
    public virtual Service Service { get; set; } = null!;
}
