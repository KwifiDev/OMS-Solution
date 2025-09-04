using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class DebtsSummary : IEntityKey
{
    [Id]
    [Column("DebtId")]
    public int Id { get; set; }

    public int? ClientId { get; set; }

    [StringLength(25)]
    public string ServiceName { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [StringLength(100)]
    public string Notes { get; set; } = null!;

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? TotalDebts { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string Status { get; set; } = null!;
}
