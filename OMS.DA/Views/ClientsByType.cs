using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMS.DA.Views;

[Keyless]
public partial class ClientsByType
{
    [StringLength(8)]
    [Unicode(false)]
    public string ClientType { get; set; } = null!;

    public int? TotalClients { get; set; }
}
