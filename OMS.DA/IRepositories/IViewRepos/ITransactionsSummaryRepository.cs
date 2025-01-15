using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ITransactionsSummaryRepository
    {
        /// <summary>
        /// Retrieves all TransactionsSummary.
        /// </summary>
        /// <returns>The task result contains the collection of TransactionsSummary.</returns>
        Task<IEnumerable<TransactionsSummary>> GetAllAsync();

        /// <summary>
        /// Retrieves an TransactionsSummary by TransactionId.
        /// </summary>
        /// <param name="transactionId">The ID of the transaction.</param>
        /// <returns>The TransactionsSummary if found; otherwise, null.</returns>
        Task<TransactionsSummary?> GetByIdAsync(int transactionId);
    }
}
