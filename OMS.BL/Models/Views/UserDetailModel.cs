namespace OMS.BL.Models.Views;

public partial class UserDetailModel
{
    public int UserId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? IsActive { get; set; }

    public string WorkingBranch { get; set; } = null!;
}
