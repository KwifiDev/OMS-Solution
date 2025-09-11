using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class UserAccount : IEntityKey
{
    [Id]
    [Column("AccountId")]
    public int Id { get; set; }

    [Column("UserAccount")]
    [StringLength(20)]
    public string UserAccount1 { get; set; } = null!;

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    public EnClientType ClientType { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal ClientBalance { get; set; }
}
