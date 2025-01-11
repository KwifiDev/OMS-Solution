using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class TransactionsSummary
{
    public int TransactionId { get; set; }

    [StringLength(20)]
    public string UserAccount { get; set; } = null!;

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
