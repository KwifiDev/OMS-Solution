using OMS.BL.Dtos.Tables;

namespace OMS.BL.IServices.Tables
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
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();

        /// <summary>
        /// Retrieves a sale by its ID asynchronously.
        /// </summary>
        /// <param name="saleId">The ID of the sale to retrieve.</param>
        /// <returns>The SaleModel object if found, otherwise null.</returns>
        Task<SaleDto?> GetSaleByIdAsync(int saleId);

        /// <summary>
        /// Adds a new sale asynchronously.
        /// </summary>
        /// <param name="dto">The SaleModel object representing the sale to add.</param>
        /// <returns>True if the sale was added successfully, otherwise false.</returns>
        Task<bool> AddSaleAsync(SaleDto dto);

        /// <summary>
        /// Updates an existing sale asynchronously.
        /// </summary>
        /// <param name="dto">The SaleModel object representing the sale to update.</param>
        /// <returns>True if the sale was updated successfully, otherwise false.</returns>
        Task<bool> UpdateSaleAsync(SaleDto dto);

        /// <summary>
        /// Deletes a sale by its ID asynchronously.
        /// </summary>
        /// <param name="saleId">The ID of the sale to delete.</param>
        /// <returns>True if the sale was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteSaleAsync(int saleId);
    }
}
