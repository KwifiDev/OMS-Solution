using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables;

public partial class ClientDto
{
    [Key]
    public int Id { get; set; }

    public required int PersonId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }
}
