using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class BranchModel
{
    [Key]
    public int BranchId { get; internal set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
