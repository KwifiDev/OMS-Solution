using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class UserAccount
{
    public int AccountId { get; set; }

    [Column("UserAccount")]
    [StringLength(20)]
    public string UserAccount1 { get; set; } = null!;

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;

    [StringLength(19)]
    [Unicode(false)]
    public string? ClientBalance { get; set; }
}
