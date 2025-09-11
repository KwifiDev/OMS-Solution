using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class DiscountsApplied : IEntityKey
{
    [Id]
    [Column("DiscountId")]
    public int Id { get; set; }

    public int ServiceId { get; set; }

    [StringLength(25)]
    public string ServiceName { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal ServicePrice { get; set; }

    public EnClientType ClientType { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiscountPercentage { get; set; }
}
