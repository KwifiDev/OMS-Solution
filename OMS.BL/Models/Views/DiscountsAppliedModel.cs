using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class DiscountsAppliedModel : IModelKey
{
    public int Id { get; set; }

    public int? ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public string ClientType { get; set; } = null!;

    public string? Discount { get; set; }
}
