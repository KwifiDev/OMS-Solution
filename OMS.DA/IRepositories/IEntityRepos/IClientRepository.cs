using OMS.Common.Enums;
using OMS.DA.Entities;

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

        /// <summary>
        /// Represents a client by their person ID.
        /// </summary>
        /// <param name="personId">The person ID of the client.</param>
        /// <returns>The Client entity.</returns>
        Task<Client?> GetByPersonIdAsync(int personId);

        /// <summary>
        /// Represents a client ID by their person ID.
        /// </summary>
        /// <param name="personId">The person ID of the client.</param>
        /// <returns>The Client ID.</returns>
        Task<int> GetIdByPersonIdAsync(int personId);
    }
}
