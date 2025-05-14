
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents a service for managing person data.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves all people asynchronously.
        /// </summary>
        /// <returns>A collection of person models.</returns>
        Task<IEnumerable<PersonModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>The person model if found, otherwise null.</returns>
        Task<PersonModel?> GetByIdAsync(int personId);

        /// <summary>
        /// find a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person to find.</param>
        /// <returns>True if the person was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int personId);

        /// <summary>
        /// Adds a new person asynchronously.
        /// </summary>
        /// <param name="model">The person model to add.</param>
        /// <returns>True if the person was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(PersonModel model);

        /// <summary>
        /// Updates an existing person asynchronously.
        /// </summary>
        /// <param name="id">The updated person id model.</param>
        /// <param name="model">The updated person model.</param>
        /// <returns>True if the person was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, PersonModel model);

        /// <summary>
        /// Deletes a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person to delete.</param>
        /// <returns>True if the person was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int personId);
    }
}
