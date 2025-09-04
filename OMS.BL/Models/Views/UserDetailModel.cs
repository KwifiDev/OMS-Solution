using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class UserDetailModel : IModelKey
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? IsActive { get; set; }

    public string WorkingBranch { get; set; } = null!;
}
