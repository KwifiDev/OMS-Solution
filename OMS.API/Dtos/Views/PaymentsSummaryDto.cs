namespace OMS.API.Dtos.Views;

public partial class PaymentsSummaryDto
{
    public int Id { get; set; }

    public string? AmountPaid { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string? Notes { get; set; }

    public string EmployeeName { get; set; } = null!;
}
