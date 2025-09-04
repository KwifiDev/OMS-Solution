using OMS.DA.Entities.Identity;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Payment : IEntityKey
{
    [Key]
    [Column("PaymentId")]
    public int Id { get; set; }

    public int AccountId { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int CreatedByUserId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Payments")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("CreatedByUserId")]
    [InverseProperty("Payments")]
    public virtual User CreatedByUser { get; set; } = null!;

    [InverseProperty("Payment")]
    public virtual ICollection<Debt> Debts { get; set; } = new List<Debt>();
}
