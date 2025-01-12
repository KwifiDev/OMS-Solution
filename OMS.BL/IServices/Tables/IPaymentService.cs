using OMS.BL.Dtos.Tables;

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
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();

        /// <summary>
        /// Retrieves a payment by its ID asynchronously.
        /// </summary>
        /// <param name="paymentId">The ID of the payment.</param>
        /// <returns>The payment model if found, otherwise null.</returns>
        Task<PaymentDto?> GetPaymentByIdAsync(int paymentId);
    }
}
