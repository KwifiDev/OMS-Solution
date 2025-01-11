using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
