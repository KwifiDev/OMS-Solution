using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ITransactionsByTypeRepository
    {
        /// <summary>
        /// Retrieves all TransactionsByType.
        /// </summary>
        /// <returns>The task result contains the collection of TransactionsByType.</returns>
        Task<PagedResult<TransactionsByType>> GetPagedAsync(PaginationParams parameters);
    }
}
