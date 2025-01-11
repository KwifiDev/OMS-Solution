namespace OMS.BL.Models.Tables;

public partial class PaymentModel
{
    public int PaymentId { get; internal set; }

    public int AccountId { get; internal set; }

    public decimal Amount { get; internal set; }

    public string? Notes { get; internal set; }

    public DateOnly CreatedAt { get; internal set; }

    public int CreatedByUserId { get; internal set; }
}
