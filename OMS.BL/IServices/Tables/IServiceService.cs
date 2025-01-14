using OMS.BL.Dtos.Tables;

namespace OMS.BL.IServices.Tables
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
        Task<IEnumerable<ServiceDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a service by its ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the service dto, or null if not found.</returns>
        Task<ServiceDto?> GetByIdAsync(int serviceId);

        /// <summary>
        /// Adds a new service asynchronously.
        /// </summary>
        /// <param name="dto">The service dto to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was added successfully.</returns>
        Task<bool> AddAsync(ServiceDto dto);

        /// <summary>
        /// Updates an existing service asynchronously.
        /// </summary>
        /// <param name="dto">The service dto to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was updated successfully.</returns>
        Task<bool> UpdateAsync(ServiceDto dto);

        /// <summary>
        /// Deletes a service by its ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the service to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the service was deleted successfully.</returns>
        Task<bool> DeleteAsync(int serviceId);
    }
}
