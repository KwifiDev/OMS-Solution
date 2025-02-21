using OMS.Common.Enums;


namespace OMS.DA.IRepositories.IEntityRepos
{
    /// <summary>
    /// Represents a repository for managing Debt entities.
    /// </summary>
    public interface IDebtRepository
    {
        /// <summary>
        /// Pays a debt by its ID asynchronously.
        /// </summary>
        /// <param name="debtId">The ID of the debt to be paid.</param>
        /// <param name="notes">Optional notes for the payment.</param>
        /// <param name="createdByUserId">The ID of the user who is making the payment.</param>
        /// <returns>The payment status.</returns>
        Task<EnPayDebtStatus> PayDebtByIdAsync(int debtId, string? notes, int createdByUserId);
    }
}
