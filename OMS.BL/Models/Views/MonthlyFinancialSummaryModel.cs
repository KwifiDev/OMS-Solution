namespace OMS.BL.Models.Views;

public partial class MonthlyFinancialSummaryModel
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    public decimal? TotalRevenue { get; set; }
}
