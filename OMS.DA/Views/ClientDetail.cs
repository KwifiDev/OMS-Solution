using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views;

[Keyless]
public partial class ClientDetail : IEntityKey
{
    [Id]
    [Column("ClientId")]
    public int Id { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;
}
