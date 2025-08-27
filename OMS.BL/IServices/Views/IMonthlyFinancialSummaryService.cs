using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

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
        Task<PagedResult<MonthlyFinancialSummaryModel>> GetPagedAsync(PaginationParams parameters);
    }
}
