namespace OMS.Common.Dtos.Views;

public partial class UserDetailDto
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public bool IsActive { get; set; }

    public string WorkingBranch { get; set; } = null!;
}
