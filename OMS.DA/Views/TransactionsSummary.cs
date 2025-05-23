﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class TransactionsSummary
{
    [Id]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

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
