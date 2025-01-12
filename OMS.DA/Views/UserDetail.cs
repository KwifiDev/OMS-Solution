using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class UserDetail
{
    public int UserId { get; set; }

    [StringLength(41)]
    public string EmployeeName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(9)]
    [Unicode(false)]
    public string? IsActive { get; set; }

    [StringLength(20)]
    public string WorkingBranch { get; set; } = null!;
}
