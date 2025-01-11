using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a service for managing permissions configurations.
    /// </summary>
    public interface IPermissionsConfigService
    {
        /// <summary>
        /// Retrieves all permissions configurations asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of permissions configurations.</returns>
        Task<IEnumerable<PermissionsConfigModel>> GetAllPermissionsConfigsAsync();

        /// <summary>
        /// Retrieves a permissions configuration by its ID asynchronously.
        /// </summary>
        /// <param name="permissionsConfigId">The ID of the permissions configuration to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the permissions configuration, or null if not found.</returns>
        Task<PermissionsConfigModel?> GetPermissionsConfigByIdAsync(int permissionsConfigId);
    }
}
