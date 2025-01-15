using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class ClientDetail
{
    [Id]
    public int ClientId { get; set; }

    [StringLength(41)]
    public string ClientName { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;
}
