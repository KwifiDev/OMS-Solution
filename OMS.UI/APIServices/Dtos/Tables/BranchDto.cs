using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIServices.Dtos.Tables;

public partial class BranchDto
{
    [Key]
    public int BranchId { get; internal set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
