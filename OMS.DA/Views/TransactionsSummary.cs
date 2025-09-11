using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
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

    public EnTransactionType TransactionType { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Amount { get; set; }

    public DateOnly CreatedAt { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }
}
