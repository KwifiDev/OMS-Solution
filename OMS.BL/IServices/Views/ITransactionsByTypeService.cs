using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving transactions by type.
    /// </summary>
    public interface ITransactionsByTypeService
    {
        /// <summary>
        /// Retrieves all transactions grouped by type asynchronously.
        /// </summary>
        /// <returns>A collection of transactions grouped by type.</returns>
        Task<PagedResult<TransactionsByTypeModel>> GetPagedAsync(PaginationParams parameters);
    }
}
