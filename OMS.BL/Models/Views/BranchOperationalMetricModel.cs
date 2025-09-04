using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class BranchOperationalMetricModel : IModelKey
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? TotalEmployees { get; set; }
}
