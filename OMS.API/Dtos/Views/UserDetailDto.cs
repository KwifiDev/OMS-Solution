namespace OMS.API.Dtos.Views;

public partial class UserDetailDto
{
    public int UserId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? IsActive { get; set; }

    public string WorkingBranch { get; set; } = null!;
}
