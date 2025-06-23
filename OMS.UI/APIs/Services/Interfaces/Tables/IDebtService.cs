

using OMS.Common.Enums;
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents the interface for managing debts.
    /// </summary>
    public interface IDebtService
    {
        /// <summary>
        /// Retrieves all debts asynchronously.
        /// </summary>
        /// <returns>A collection of DebtModel objects.</returns>
        Task<IEnumerable<DebtModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a debt by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt.</param>
        /// <returns>The DebtModel object if found, otherwise null.</returns>
        Task<DebtModel?> GetByIdAsync(int debtId);

        /// <summary>
        /// find a debt by their ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt to find.</param>
        /// <returns>True if the debt was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int debtId);

        /// <summary>
        /// Adds a new debt asynchronously.
        /// </summary>
        /// <param name="model">The DebtModel object representing the new debt.</param>
        /// <returns>True if the debt was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(DebtModel model);

        /// <summary>
        /// Updates an existing debt asynchronously.
        /// </summary>
        /// <param name="id">The Debt id</param>
        /// <param name="model">The DebtModel object representing the updated debt.</param>
        /// <returns>True if the debt was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, DebtModel model);

        /// <summary>
        /// Deletes a debt by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt to delete.</param>
        /// <returns>True if the debt was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int debtId);

        /// <summary>
        /// Pays a debt by its ID asynchronously.
        /// </summary>
        /// <param name="model">The PayDebtModel object representing the payment details.</param>
        /// <returns>True if the debt was paid successfully, otherwise false.</returns>
        Task<EnPayDebtStatus> PayDebtAsync(PayDebtModel model);

        /// <summary>
        /// Create a Debt record with fully computed columns then insert it into Debts table.
        /// </summary>
        /// <param name="model">The Create Debt Model object representing the args of SP on SQLSERVER to create new Debt</param>
        /// <returns>True if the Debt was created successfully, otherwise false.</returns>
        Task<bool> AddDebtAsync(DebtCreationModel model);

        /// <summary>
        /// Updates a Debt to became canceled asynchronously.
        /// </summary>
        /// <param name="debtId">The Debt Id representing the Debt to cancel.</param>
        /// <returns>True if the Debt was canceled successfully, otherwise false.</returns>
        Task<bool> CancelDebtAsync(int debtId);
    }
}
