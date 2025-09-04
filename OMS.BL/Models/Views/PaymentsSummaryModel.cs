using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class PaymentsSummaryModel : IModelKey
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string? AmountPaid { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string? Notes { get; set; }

    public string EmployeeName { get; set; } = null!;
}
