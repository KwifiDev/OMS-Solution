using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class ClientDto
{
    [Key]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Person Id must be positive number")]
    public int PersonId { get; set; }

    [Range(0, 2, ErrorMessage = "Client Type must be between (0:Normal, 1:Lawyer, 2:Other)")]
    public EnClientType ClientType { get; set; }
}
