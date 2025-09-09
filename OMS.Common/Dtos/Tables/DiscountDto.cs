using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables;

public partial class DiscountDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "ServiceId is Required")]
    public required int ServiceId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    [Required(ErrorMessage = "ClientType is Required")]
    [Range(0, 2, ErrorMessage = "ClientType Must Be Between [0 - 2]")]
    public required EnClientType ClientType { get; set; }

    [Required(ErrorMessage = "ClientType is Required")]
    [Range(1, 99, ErrorMessage = "DiscountPercentage must be Between [1 - 99]")]
    public required decimal DiscountPercentage { get; set; }
}
