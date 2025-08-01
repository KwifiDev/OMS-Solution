﻿using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class PaymentsSummary
{
    [Id]
    public int PaymentId { get; set; }

    public int AccountId { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal AmountPaid { get; set; }

    public DateOnly CreatedAt { get; set; }

    [StringLength(100)]
    public string? Notes { get; set; }

    [StringLength(41)]
    public string EmployeeName { get; set; } = null!;
}
