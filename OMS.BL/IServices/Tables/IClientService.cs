using OMS.BL.Models.StoredProcedureParams;
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
        Task<IEnumerable<ClientModel>> GetAllAsync();

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
        /// <param name="model">The client model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was updated successfully.</returns>
        Task<bool> UpdateAsync(ClientModel model);

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
        Task<bool> PayAllDebtsById(PayDebtsModel model);
    }
}
