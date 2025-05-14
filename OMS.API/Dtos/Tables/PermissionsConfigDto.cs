using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class PermissionsConfigDto
{
    [Key]
    public int PermissionConfigId { get; set; }

    public string PermissionName { get; set; } = null!;

    /// <summary>
    /// Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...
    /// </summary>
    public int PermissionNo { get; set; }
}
