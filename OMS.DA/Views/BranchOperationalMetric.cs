﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class BranchOperationalMetric
{
    public int BranchId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    public int? TotalEmployees { get; set; }
}
