namespace OMS.BL.Dtos.Tables;


public partial class ServiceDto
{
    public int ServiceId { get; internal set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }
}
