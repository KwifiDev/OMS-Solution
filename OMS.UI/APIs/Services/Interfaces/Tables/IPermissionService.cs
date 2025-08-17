using OMS.UI.Models.Tables;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    public interface IPermissionService
    {
        /// <summary>
        /// Retrieves all permissions asynchronously.
        /// </summary>
        /// <returns>A collection of permission models.</returns>
        Task<IEnumerable<PermissionModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a permission by their ID asynchronously.
        /// </summary>
        /// <param name="permissionId">The ID of the permission.</param>
        /// <returns>The permission model if found, otherwise null.</returns>
        Task<PermissionModel?> GetByIdAsync(int permissionId);

        /// <summary>
        /// find a permission by their ID asynchronously.
        /// </summary>
        /// <param name="permissionId">The ID of the permission to find.</param>
        /// <returns>True if the permission was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int permissionId);

        /// <summary>
        /// Adds a new permission asynchronously.
        /// </summary>
        /// <param name="model">The permission model to add.</param>
        /// <returns>True if the permission was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(PermissionModel model);

        /// <summary>
        /// Updates an existing permission asynchronously.
        /// </summary>
        /// <param name="id">The updated permission id model.</param>
        /// <param name="model">The updated permission model.</param>
        /// <returns>True if the permission was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, PermissionModel model);

        /// <summary>
        /// Deletes a permission by their ID asynchronously.
        /// </summary>
        /// <param name="permissionId">The ID of the permission to delete.</param>
        /// <returns>True if the permission was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int permissionId);
    }
}
