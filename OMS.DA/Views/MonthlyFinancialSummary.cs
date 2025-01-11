using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class MonthlyFinancialSummary
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    [Column(TypeName = "decimal(38, 2)")]
    public decimal? TotalRevenue { get; set; }
}
