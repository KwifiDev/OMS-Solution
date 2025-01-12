using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class DebtsByStatus
{
    [StringLength(9)]
    [Unicode(false)]
    public string DebtsStatus { get; set; } = null!;

    public int? TotalDebts { get; set; }

    [StringLength(34)]
    [Unicode(false)]
    public string? TotalAmount { get; set; }
}
