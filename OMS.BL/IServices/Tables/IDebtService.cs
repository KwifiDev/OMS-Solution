using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;

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
        Task<IEnumerable<DebtDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a debt by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt.</param>
        /// <returns>The DebtModel object if found, otherwise null.</returns>
        Task<DebtDto?> GetByIdAsync(int debtId);

        /// <summary>
        /// Adds a new debt asynchronously.
        /// </summary>
        /// <param name="dto">The DebtModel object representing the new debt.</param>
        /// <returns>True if the debt was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(DebtDto dto);

        /// <summary>
        /// Updates an existing debt asynchronously.
        /// </summary>
        /// <param name="dto">The DebtModel object representing the updated debt.</param>
        /// <returns>True if the debt was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(DebtDto dto);

        /// <summary>
        /// Deletes a debt by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt to delete.</param>
        /// <returns>True if the debt was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int debtId);

        /// <summary>
        /// Pays a debt by its ID asynchronously.
        /// </summary>
        /// <param name="dto">The PayDebtModel object representing the payment details.</param>
        /// <returns>True if the debt was paid successfully, otherwise false.</returns>
        Task<bool> PayDebtByIdAsync(PayDebtDto dto);
    }
}
