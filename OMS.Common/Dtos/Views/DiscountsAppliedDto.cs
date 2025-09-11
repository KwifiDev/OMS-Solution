using OMS.Common.Enums;

namespace OMS.Common.Dtos.Views;

public partial class DiscountsAppliedDto
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal ServicePrice { get; set; }

    public EnClientType ClientType { get; set; }

    public decimal DiscountPercentage { get; set; }
}
