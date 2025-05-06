using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a service for managing payments.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Retrieves all payments asynchronously.
        /// </summary>
        /// <returns>A collection of payment models.</returns>
        Task<IEnumerable<PaymentModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a payment by its ID asynchronously.
        /// </summary>
        /// <param name="paymentId">The ID of the payment.</param>
        /// <returns>The payment model if found, otherwise null.</returns>
        Task<PaymentModel?> GetByIdAsync(int paymentId);
    }
}
