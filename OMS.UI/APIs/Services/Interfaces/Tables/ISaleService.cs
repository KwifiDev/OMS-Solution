﻿using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;

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

        /// <summary>
        /// Create a sale record with fully computed columns then insert it into sales table.
        /// </summary>
        /// <param name="model">The Create Sale Model object representing the args of SP on SQLSERVER to create new sale</param>
        /// <returns>True if the sale was created successfully, otherwise false.</returns>
        Task<bool> AddSaleAsync(SaleCreationModel model);

        /// <summary>
        /// Updates a sale to became canceled asynchronously.
        /// </summary>
        /// <param name="saleId">The sale Id representing the sale to cancel.</param>
        /// <returns>True if the sale was canceled successfully, otherwise false.</returns>
        Task<bool> CancelSaleAsync(int saleId);
    }
}
