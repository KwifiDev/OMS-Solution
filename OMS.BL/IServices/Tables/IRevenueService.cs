﻿using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a service for managing revenue data.
    /// </summary>
    public interface IRevenueService
    {
        /// <summary>
        /// Retrieves all revenues asynchronously.
        /// </summary>
        /// <returns>A collection of revenue models.</returns>
        Task<IEnumerable<RevenueModel>> GetAllRevenuesAsync();

        /// <summary>
        /// Retrieves a revenue by its ID asynchronously.
        /// </summary>
        /// <param name="revenueId">The ID of the revenue to retrieve.</param>
        /// <returns>The revenue model, or null if not found.</returns>
        Task<RevenueModel?> GetRevenueByIdAsync(int revenueId);

        /// <summary>
        /// Adds a new revenue asynchronously.
        /// </summary>
        /// <param name="model">The revenue model to add.</param>
        /// <returns>True if the revenue was added successfully, otherwise false.</returns>
        Task<bool> AddRevenueAsync(RevenueModel model);

        /// <summary>
        /// Updates an existing revenue asynchronously.
        /// </summary>
        /// <param name="model">The revenue model to update.</param>
        /// <returns>True if the revenue was updated successfully, otherwise false.</returns>
        Task<bool> UpdateRevenueAsync(RevenueModel model);

        /// <summary>
        /// Deletes a revenue by its ID asynchronously.
        /// </summary>
        /// <param name="revenueId">The ID of the revenue to delete.</param>
        /// <returns>True if the revenue was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteRevenueAsync(int revenueId);
    }
}
