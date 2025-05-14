using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class ClientDto
{
    [Key]
    public int ClientId { get; set; }

    public required int PersonId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }
}
