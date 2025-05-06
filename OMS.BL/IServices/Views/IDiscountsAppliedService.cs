using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for managing discounts applied.
    /// </summary>
    public interface IDiscountsAppliedService
    {
        /// <summary>
        /// Retrieves all discounts applied asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of discounts applied.</returns>
        Task<IEnumerable<DiscountsAppliedModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a discount applied by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount applied.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the discount applied with the specified ID, or null if not found.</returns>
        Task<DiscountsAppliedModel?> GetByIdAsync(int discountId);
    }
}
