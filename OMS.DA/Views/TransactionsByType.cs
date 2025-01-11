using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class TransactionsByType
{
    [StringLength(8)]
    [Unicode(false)]
    public string TransactionType { get; set; } = null!;

    public int? TotalTransactions { get; set; }

    [StringLength(34)]
    [Unicode(false)]
    public string? TotalAmount { get; set; }
}
