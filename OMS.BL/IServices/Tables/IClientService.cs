using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;

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
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();

        /// <summary>
        /// Retrieves a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client dto, or null if not found.</returns>
        Task<ClientDto?> GetClientByIdAsync(int clientId);

        /// <summary>
        /// Adds a new client asynchronously.
        /// </summary>
        /// <param name="dto">The client dto to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was added successfully.</returns>
        Task<bool> AddClientAsync(ClientDto dto);

        /// <summary>
        /// Updates an existing client asynchronously.
        /// </summary>
        /// <param name="dto">The client dto to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was updated successfully.</returns>
        Task<bool> UpdateClientAsync(ClientDto dto);

        /// <summary>
        /// Deletes a client by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the client was deleted successfully.</returns>
        Task<bool> DeleteClientAsync(int clientId);

        /// <summary>
        /// Pays all debts for a client by ID asynchronously.
        /// </summary>
        /// <param name="dto">The payment dto containing the client ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the debts were paid successfully.</returns>
        Task<bool> PayAllDebtsById(PayDebtsDto dto);
    }
}
