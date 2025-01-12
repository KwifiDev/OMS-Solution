namespace OMS.BL.Dtos.Tables;

public partial class BranchDto
{
    public int BranchId { get; internal set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
