
namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents the interface for managing discounts.
    /// </summary>
    public interface IDiscountService
    {

        /// <summary>
        /// Retrieves all discounts asynchronously.
        /// </summary>
        /// <returns>A collection of discount models.</returns>
        //Task<IEnumerable<DiscountModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a discount by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount.</param>
        /// <returns>The discount model if found, otherwise null.</returns>
        //Task<DiscountModel?> GetByIdAsync(int discountId);

        /// <summary>
        /// Adds a new discount asynchronously.
        /// </summary>
        /// <param name="model">The discount model to add.</param>
        /// <returns>True if the discount was added successfully, otherwise false.</returns>
        //Task<bool> AddAsync(DiscountModel model);

        /// <summary>
        /// Updates an existing discount asynchronously.
        /// </summary>
        /// <param name="id">The discount model id to update.</param>
        /// <param name="model">The discount model to update.</param>
        /// <returns>True if the discount was updated successfully, otherwise false.</returns>
        //Task<bool> UpdateAsync(int id, DiscountModel model);

        /// <summary>
        /// Deletes a discount by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount to delete.</param>
        /// <returns>True if the discount was deleted successfully, otherwise false.</returns>
        //Task<bool> DeleteAsync(int discountId);
    }
}
