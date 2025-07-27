using OMS.UI.Models.Views;
using OMS.UI.Services.ModelTransfer;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for managing discounts applied.
    /// </summary>
    public interface IDiscountsAppliedService : IDisplayService<DiscountsAppliedModel>
    {
        /// <summary>
        /// Retrieves all discounts applied asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of discounts applied.</returns>
        //Task<IEnumerable<DiscountsAppliedModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a discount applied by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount applied.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the discount applied with the specified ID, or null if not found.</returns>
        //Task<DiscountsAppliedModel?> GetByIdAsync(int discountId);

        /// <summary>
        /// Retrieves discounts applied by service asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of discounts applied.</returns>
        Task<IEnumerable<DiscountsAppliedModel>> GetDiscountsByServiceIdAsync(int serviceId);
    }
}
