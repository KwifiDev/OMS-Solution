using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Entities;

[Table("PermissionsConfig")]
public partial class PermissionsConfig
{
    [Key]
    public int PermissionConfigId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PermissionName { get; set; } = null!;

    /// <summary>
    /// Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...
    /// </summary>
    public int PermissionNo { get; set; }
}
