using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents a service service that provides operations for managing services.
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Retrieves all services asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of service models.</returns>
        Task<IEnumerable<ServiceModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a service by its ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the service model, or null if not found.</returns>
        Task<ServiceModel?> GetByIdAsync(int serviceId);

        /// <summary>
        /// Adds a new service asynchronously.
        /// </summary>
        /// <param name="model">The service model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was added successfully.</returns>
        Task<bool> AddAsync(ServiceModel model);

        /// <summary>
        /// Updates an existing service asynchronously.
        /// </summary>
        /// <param name="id">The service id of model.</param>
        /// <param name="model">The service model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was updated successfully.</returns>
        Task<bool> UpdateAsync(int id, ServiceModel model);

        /// <summary>
        /// Deletes a service by its ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was deleted successfully.</returns>
        Task<bool> DeleteAsync(int serviceId);

        /// <summary>
        /// Retrieves all Services Option asynchronously.
        /// </summary>
        /// <returns>A collection of Service option.</returns>
        Task<IEnumerable<ServiceOptionModel>> GetAllServicesOption();
    }
}
