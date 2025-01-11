namespace OMS.BL.Models.Views;

public partial class PaymentsSummaryModel
{
    public int PaymentId { get; set; }

    public string ClientName { get; set; } = null!;

    public string? AmountPaid { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Notes { get; set; } = null!;
}
