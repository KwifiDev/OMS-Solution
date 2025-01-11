﻿using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
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
        Task<IEnumerable<DiscountModel>> GetAllDiscountsAsync();

        /// <summary>
        /// Retrieves a discount by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount.</param>
        /// <returns>The discount model if found, otherwise null.</returns>
        Task<DiscountModel?> GetDiscountByIdAsync(int discountId);

        /// <summary>
        /// Adds a new discount asynchronously.
        /// </summary>
        /// <param name="model">The discount model to add.</param>
        /// <returns>True if the discount was added successfully, otherwise false.</returns>
        Task<bool> AddDiscountAsync(DiscountModel model);

        /// <summary>
        /// Updates an existing discount asynchronously.
        /// </summary>
        /// <param name="model">The discount model to update.</param>
        /// <returns>True if the discount was updated successfully, otherwise false.</returns>
        Task<bool> UpdateDiscountAsync(DiscountModel model);

        /// <summary>
        /// Deletes a discount by its ID asynchronously.
        /// </summary>
        /// <param name="discountId">The ID of the discount to delete.</param>
        /// <returns>True if the discount was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteDiscountAsync(int discountId);
    }
}
