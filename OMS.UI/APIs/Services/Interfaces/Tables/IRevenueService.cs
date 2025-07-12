
using OMS.UI.Models;
using OMS.UI.Services.ModelTransfer;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents a service for managing revenue data.
    /// </summary>
    public interface IRevenueService : IDisplayService<RevenueModel>
    {
        /// <summary>
        /// Retrieves all revenues asynchronously.
        /// </summary>
        /// <returns>A collection of revenue models.</returns>
        //Task<IEnumerable<RevenueModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a revenue by its ID asynchronously.
        /// </summary>
        /// <param name="revenueId">The ID of the revenue to retrieve.</param>
        /// <returns>The revenue model, or null if not found.</returns>
        //Task<RevenueModel?> GetByIdAsync(int revenueId);

        /// <summary>
        /// Adds a new revenue asynchronously.
        /// </summary>
        /// <param name="model">The revenue model to add.</param>
        /// <returns>True if the revenue was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(RevenueModel model);

        /// <summary>
        /// Updates an existing revenue asynchronously.
        /// </summary>
        /// <param name="id">The revenue id to update.</param>
        /// <param name="model">The revenue model to update.</param>
        /// <returns>True if the revenue was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, RevenueModel model);

        /// <summary>
        /// Deletes a revenue by its ID asynchronously.
        /// </summary>
        /// <param name="revenueId">The ID of the revenue to delete.</param>
        /// <returns>True if the revenue was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int revenueId);
    }
}
