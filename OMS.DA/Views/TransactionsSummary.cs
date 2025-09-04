using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class TransactionsSummary : IEntityKey
{
    [Id]
    [Column("TransactionId")]
    public int Id { get; set; }

    public int AccountId { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string TransactionType { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string? Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    [StringLength(100)]
    public string Notes { get; set; } = null!;
}
