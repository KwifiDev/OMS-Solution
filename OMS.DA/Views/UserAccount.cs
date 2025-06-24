using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class UserAccount
{
    [Id]
    public int AccountId { get; set; }

    [Column("UserAccount")]
    [StringLength(20)]
    public string UserAccount1 { get; set; } = null!;

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;

    [Column(TypeName = "decimal(8, 2)")]
    public decimal ClientBalance { get; set; }
}
