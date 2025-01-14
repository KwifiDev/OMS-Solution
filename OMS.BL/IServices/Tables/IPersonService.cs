using OMS.BL.Dtos.Tables;

namespace OMS.BL.IServices.Tables
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
        Task<IEnumerable<PersonDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>The person dto if found, otherwise null.</returns>
        Task<PersonDto?> GetByIdAsync(int personId);

        /// <summary>
        /// Adds a new person asynchronously.
        /// </summary>
        /// <param name="dto">The person dto to add.</param>
        /// <returns>True if the person was added successfully, otherwise false.</returns>
        Task<bool> AddAsync(PersonDto dto);

        /// <summary>
        /// Updates an existing person asynchronously.
        /// </summary>
        /// <param name="dto">The updated person dto.</param>
        /// <returns>True if the person was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAsync(PersonDto dto);

        /// <summary>
        /// Deletes a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person to delete.</param>
        /// <returns>True if the person was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(int personId);
    }
}
