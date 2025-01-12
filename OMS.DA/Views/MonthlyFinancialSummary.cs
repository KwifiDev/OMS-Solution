using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class MonthlyFinancialSummary
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalRevenue { get; set; }
}
