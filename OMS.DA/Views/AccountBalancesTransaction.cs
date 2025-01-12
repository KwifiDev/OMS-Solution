using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class AccountBalancesTransaction
{
    public int AccountId { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(20)]
    public string UserAccount { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string? AccountBalance { get; set; }

    public int? TotalTransactions { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalTransactionAmount { get; set; }
}
