namespace OMS.BL.Models.Tables;

public partial class BranchModel
{
    public int BranchId { get; internal set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
}
