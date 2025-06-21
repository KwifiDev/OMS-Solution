using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
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
        /// <param name="model">The DebtModel object representing the updated debt.</param>
        /// <returns>True if the debt was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(DebtModel model);

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
        Task<bool> PayDebtByIdAsync(PayDebtModel model);

        /// <summary>
        /// Create a debt record with fully computed columns then insert it into debts table.
        /// </summary>
        /// <param name="model">The Create debt Model object representing the args of SP on SQLSERVER to create new debt</param>
        /// <returns>True if the debt was created successfully, otherwise false.</returns>
        Task<bool> AddDebtAsync(DebtCreationModel model);

        /// <summary>
        /// Updates a debt to became canceled asynchronously.
        /// </summary>
        /// <param name="debtId">The debt Id representing the debt to cancel.</param>
        /// <returns>True if the debt was canceled successfully, otherwise false.</returns>
        Task<bool?> CancelDebtAsync(int debtId);
    }
}
