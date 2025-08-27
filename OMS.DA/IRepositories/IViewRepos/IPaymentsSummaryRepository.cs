using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IPaymentsSummaryRepository
    {
        /// <summary>
        /// Retrieves all PaymentsSummary.
        /// </summary>
        /// <returns>The task result contains the collection of PaymentsSummary.</returns>
        Task<PagedResult<PaymentsSummary>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an PaymentsSummary by PaymentId.
        /// </summary>
        /// <param name="paymentId">The ID of the payment.</param>
        /// <returns>The PaymentsSummary if found; otherwise, null.</returns>
        Task<PaymentsSummary?> GetByIdAsync(int paymentId);

        /// <summary>
        /// Retrieves all PaymentsSummary by Account Id.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The task result contains the collection of PaymentsSummary by Account Id.</returns>
        Task<PagedResult<PaymentsSummary>> GetPaymentsByAccountIdPagedAsync(int accountId, PaginationParams parameters);
    }
}
