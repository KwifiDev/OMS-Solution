
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents the interface for managing sales.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Retrieves all sales asynchronously.
        /// </summary>
        /// <returns>A collection of SaleModel objects.</returns>
        Task<IEnumerable<SaleModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a sale by its ID asynchronously.
        /// </summary>
        /// <param name="saleId">The ID of the sale to retrieve.</param>
        /// <returns>The SaleModel object if found, otherwise null.</returns>
        Task<SaleModel?> GetByIdAsync(int saleId);

        /// <summary>
        /// Adds a new sale asynchronously.
        /// </summary>
        /// <param name="model">The SaleModel object representing the sale to add.</param>
        /// <returns>True if the sale was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(SaleModel model);

        /// <summary>
        /// Updates an existing sale asynchronously.
        /// </summary>
        /// <param name="id">The Sale id</param>
        /// <param name="model">The SaleModel object representing the sale to update.</param>
        /// <returns>True if the sale was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, SaleModel model);

        /// <summary>
        /// Deletes a sale by its ID asynchronously.
        /// </summary>
        /// <param name="saleId">The ID of the sale to delete.</param>
        /// <returns>True if the sale was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int saleId);
    }
}
