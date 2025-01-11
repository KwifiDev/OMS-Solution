namespace OMS.BL.Models.Tables;

public partial class UserModel
{
    public int UserId { get; internal set; }

    public required int PersonId { get; set; }

    public required int BranchId { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public required int Permissions { get; set; }

    public bool IsActive { get; set; } = false;

    // ===========================================================
    public string IsActiveText { get => IsActive ? "نشط" : "غير نشط"; }
}
