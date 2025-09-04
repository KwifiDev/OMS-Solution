using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables;

public partial class BranchDto
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
