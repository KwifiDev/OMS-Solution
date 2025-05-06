namespace OMS.BL.Models.Views;

public partial class DebtsByStatusModel
{
    public string DebtsStatus { get; set; } = null!;

    public int? TotalDebts { get; set; }

    public string? TotalAmount { get; set; }
}
