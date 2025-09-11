using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class DiscountsAppliedModel : IModelKey
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public EnClientType ClientType { get; set; }

    public decimal DiscountPercentage { get; set; }
}
