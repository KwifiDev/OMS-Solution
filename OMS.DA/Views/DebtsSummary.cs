using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
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

    public int ClientId { get; set; }

    [StringLength(25)]
    public string ServiceName { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? TotalDebts { get; set; }

    public EnDebtStatus Status { get; set; }

    public DateOnly CreatedAt { get; set; }
}
