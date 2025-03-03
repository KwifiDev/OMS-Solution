using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views;

[Keyless]
public partial class BranchOption
{
    [Id]
    public int BranchId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;
}
