namespace OMS.BL.Models.Views;

public partial class BranchOperationalMetricModel
{
    public int BranchId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? TotalEmployees { get; set; }
}
