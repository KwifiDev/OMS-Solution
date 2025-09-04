using Microsoft.EntityFrameworkCore;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Entities;

[Index("CreatedAt", Name = "revenues_createdat_unique", IsUnique = true)]
public partial class Revenue : IEntityKey
{
    [Key]
    [Column("RevenueId")]
    public int Id { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; set; }
}
