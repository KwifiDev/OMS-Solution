using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving monthly financial summaries.
    /// </summary>
    public interface IMonthlyFinancialSummaryService
    {
        /// <summary>
        /// Retrieves all monthly financial summaries asynchronously.
        /// </summary>
        /// <returns>A collection of MonthlyFinancialSummaryModel objects.</returns>
        Task<IEnumerable<MonthlyFinancialSummaryModel>> GetAllMonthlyFinancialSummariesAsync();
    }
}
