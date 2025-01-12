using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class ClientsByType
{
    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;

    public int? TotalClients { get; set; }
}
