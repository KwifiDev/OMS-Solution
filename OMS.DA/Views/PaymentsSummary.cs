﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class PaymentsSummary
{
    public int PaymentId { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string? AmountPaid { get; set; }

    public DateOnly CreatedAt { get; set; }

    [StringLength(100)]
    public string Notes { get; set; } = null!;
}
