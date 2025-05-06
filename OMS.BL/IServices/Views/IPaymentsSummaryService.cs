using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
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
    }
}
