using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables;

public partial class PersonDto
{
    [Key]
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    /// <summary>
    /// 0 = Male | 1 = Female
    /// </summary>
    public required EnGender Gender { get; set; }

    public string? Phone { get; set; }

}
