using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class AccountBalancesTransaction : IEntityKey
{
    [Id]
    [Column("AccountId")]
    public int Id { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(20)]
    public string UserAccount { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal AccountBalance { get; set; }

    public int? TotalTransactions { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalTransactionAmount { get; set; }
}
