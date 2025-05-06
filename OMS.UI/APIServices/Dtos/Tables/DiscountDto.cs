using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIServices.Dtos.Tables;

public partial class DiscountDto
{
    [Key]
    public int DiscountId { get; internal set; }

    public required int ServiceId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }

    public required decimal DiscountPercentage { get; set; }
}
