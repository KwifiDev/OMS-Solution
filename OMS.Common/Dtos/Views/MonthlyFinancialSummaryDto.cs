namespace OMS.Common.Dtos.Views;

public partial class MonthlyFinancialSummaryDto
{
    public int? Year { get; set; }

    public int? Month { get; set; }

    public decimal? TotalRevenue { get; set; }
}
