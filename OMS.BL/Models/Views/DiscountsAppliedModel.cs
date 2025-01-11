namespace OMS.BL.Models.Views;

public partial class DiscountsAppliedModel
{
    public int DiscountId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public string ClientType { get; set; } = null!;

    public string? Discount { get; set; }
}
