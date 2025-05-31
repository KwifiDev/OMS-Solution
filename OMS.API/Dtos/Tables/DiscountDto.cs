using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables;

public partial class DiscountDto
{
    [Key]
    public int DiscountId { get; set; }

    [Required(ErrorMessage = "ServiceId is Required")]
    public required int ServiceId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    [Required(ErrorMessage = "ClientType is Required")]
    [Range(0, 2, ErrorMessage = "ClientType Must Be Between [0 - 2]")]
    public required EnClientType ClientType { get; set; }

    [Required(ErrorMessage = "ClientType is Required")]
    [Range(1, 100, ErrorMessage = "DiscountPercentage must be Between [1 - 100]")]
    public required decimal DiscountPercentage { get; set; }
}
