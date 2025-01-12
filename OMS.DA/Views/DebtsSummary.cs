using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class DebtsSummary
{
    public int DebtId { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(25)]
    public string ServiceName { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string? TotalDebts { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string Status { get; set; } = null!;
}
