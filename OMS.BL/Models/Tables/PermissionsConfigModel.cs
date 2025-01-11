namespace OMS.BL.Models.Tables;

public partial class PermissionsConfigModel
{
    public int PermissionConfigId { get; internal set; }

    public string PermissionName { get; internal set; } = null!;

    /// <summary>
    /// Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...
    /// </summary>
    public int PermissionNo { get; internal set; }
}
