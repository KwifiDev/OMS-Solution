
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for retrieving payments summary information.
    /// </summary>
    public interface IPaymentsSummaryService
    {
        /// <summary>
        /// Retrieves all payments summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of PaymentsSummaryModel.</returns>
        Task<IEnumerable<PaymentsSummaryModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a payment summary by its ID asynchronously.
        /// </summary>
        /// <param name="paymentId">The ID of the payment summary to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a PaymentsSummaryModel object, or null if no payment summary is found with the specified ID.</returns>
        Task<PaymentsSummaryModel?> GetByIdAsync(int paymentId);

        /// <summary>
        /// Retrieves all PaymentsSummary by Account Id.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The task result contains the collection of PaymentsSummary by Account Id.</returns>
        Task<IEnumerable<PaymentsSummaryModel>> GetPaymentsByAccountIdAsync(int accountId);
    }
}
