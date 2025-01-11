using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ITransactionsSummaryRepository : IGenericViewRepository<TransactionsSummary>
    {
        /// <summary>
        /// Retrieves an TransactionsSummary by TransactionId.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>The TransactionsSummary if found; otherwise, null.</returns>
        Task<TransactionsSummary?> GetTransactionSummaryByIdAsync(int transactionId);
    }
}
