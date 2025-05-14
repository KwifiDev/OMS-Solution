namespace OMS.UI.APIs.Dtos.Views;

public partial class DebtsByStatusDto
{
    public string DebtsStatus { get; set; } = null!;

    public int? TotalDebts { get; set; }

    public string? TotalAmount { get; set; }
}
