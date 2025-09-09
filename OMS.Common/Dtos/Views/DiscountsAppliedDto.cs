namespace OMS.Common.Dtos.Views;

public partial class DiscountsAppliedDto
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public string ClientType { get; set; } = null!;

    public string? Discount { get; set; }
}
