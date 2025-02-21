using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

public partial class Sale
{
    [Key]
    public int SaleId { get; set; }

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
    /// 0 = Uncompleted | 1 = Completed | 2 = Canceled
    /// </summary>
    public EnSaleStatus Status { get; set; }

    public int CreatedByUserId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Sales")]
    public virtual Client Client { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("Sales")]
    public virtual Service Service { get; set; } = null!;
}
