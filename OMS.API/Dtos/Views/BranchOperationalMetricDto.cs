namespace OMS.API.Dtos.Views;

public partial class BranchOperationalMetricDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? TotalEmployees { get; set; }
}
