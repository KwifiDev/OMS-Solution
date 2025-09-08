
using OMS.Common.Enums;
using OMS.Common.Extensions.Pagination;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents the interface for managing client data.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Retrieves all clients asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of client models.</returns>
        Task<PagedResult<ClientModel>?> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client model, or null if not found.</returns>
        Task<ClientModel?> GetByIdAsync(int clientId);

        /// <summary>
        /// find a client by their ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to find.</param>
        /// <returns>True if the client was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int clientId);

        /// <summary>
        /// Retrieves a client by person ID asynchronously.
        /// </summary>
        /// <param name="personId">The person id of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client model, or null if not found.</returns>
        Task<ClientModel?> GetByPersonIdAsync(int personId);

        /// <summary>
        /// Retrieves a client ID by person ID asynchronously.
        /// </summary>
        /// <param name="personId">The person id of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client ID, or null if not found.</returns>
        Task<int> GetIdByPersonIdAsync(int personId);

        /// <summary>
        /// Adds a new client asynchronously.
        /// </summary>
        /// <param name="model">The client model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was added successfully.</returns>
        Task<bool> AddAsync(ClientModel model);

        /// <summary>
        /// Updates an existing client asynchronously.
        /// </summary>
        /// <param name="id">The client model id to update.</param>
        /// <param name="model">The client model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was updated successfully.</returns>
        Task<bool> UpdateAsync(int id, ClientModel model);

        /// <summary>
        /// Deletes a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was deleted successfully.</returns>
        Task<bool> DeleteAsync(int clientId);

        /// <summary>
        /// Pays all debts for a client by ID asynchronously.
        /// </summary>
        /// <param name="model">The payment model containing the client ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the debts were paid successfully.</returns>
        Task<EnPayDebtStatus> PayAllDebtsById(PayDebtsModel model);


        /// <summary>
        /// Adds a new client with account asynchronously.
        /// </summary>
        /// <param name="model">The client & account model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client & account was added successfully.</returns>
        Task<bool> AddWithAccountAsync(ClientAccountModel model);

        /// <summary>
        /// updates a new client with account asynchronously.
        /// </summary>
        /// <param name="model">The client & account model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client & account was added successfully.</returns>
        Task<bool> UpdateWithAccountAsync(ClientAccountModel model);
    }
}
