using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
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
        Task<IEnumerable<ClientModel>> GetAllClientsAsync();

        /// <summary>
        /// Retrieves a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client model, or null if not found.</returns>
        Task<ClientModel?> GetClientByIdAsync(int clientId);

        /// <summary>
        /// Adds a new client asynchronously.
        /// </summary>
        /// <param name="model">The client model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was added successfully.</returns>
        Task<bool> AddClientAsync(ClientModel model);

        /// <summary>
        /// Updates an existing client asynchronously.
        /// </summary>
        /// <param name="model">The client model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was updated successfully.</returns>
        Task<bool> UpdateClientAsync(ClientModel model);

        /// <summary>
        /// Deletes a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was deleted successfully.</returns>
        Task<bool> DeleteClientAsync(int clientId);

        /// <summary>
        /// Pays all debts for a client by ID asynchronously.
        /// </summary>
        /// <param name="model">The payment model containing the client ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the debts were paid successfully.</returns>
        Task<bool> PayAllDebtsById(PayDebtsModel model);
    }
}
