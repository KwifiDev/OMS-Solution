using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables;

public partial class UserDto
{
    [Key]
    public int UserId { get; set; }

    public required int PersonId { get; set; }

    public required int BranchId { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public bool IsActive { get; set; } = false;
}
