using OMS.Common.Enums;

namespace OMS.DA.IRepositories.IEntityRepos
{
    /// <summary>
    /// Represents a repository for managing clients.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Pays all debts for a client by their ID.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="notes">Optional notes for the payment.</param>
        /// <param name="createdByUserId">The ID of the user who created the payment.</param>
        /// <returns>The payment debt status.</returns>
        Task<EnPayDebtStatus> PayAllDebtsByIdAsync(int clientId, string? notes, int createdByUserId);
    }
}
