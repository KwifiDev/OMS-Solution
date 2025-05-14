using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class BranchDto
{
    [Key]
    public int BranchId { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
