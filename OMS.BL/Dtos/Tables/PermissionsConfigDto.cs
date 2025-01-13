using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Dtos.Tables;

public partial class PermissionsConfigDto
{
    [Key]
    public int PermissionConfigId { get; internal set; }

    public string PermissionName { get; internal set; } = null!;

    /// <summary>
    /// Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...
    /// </summary>
    public int PermissionNo { get; internal set; }
}
