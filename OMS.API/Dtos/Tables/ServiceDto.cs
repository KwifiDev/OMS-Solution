using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;


public partial class ServiceDto
{
    [Key]
    public int ServiceId { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }
}
