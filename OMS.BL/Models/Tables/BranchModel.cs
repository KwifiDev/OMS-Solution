using OMS.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class BranchModel : IModelKey
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
