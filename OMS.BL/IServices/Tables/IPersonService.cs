using OMS.BL.Models.Tables;

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
        Task<IEnumerable<PersonModel>> GetAllPeopleAsync();

        /// <summary>
        /// Retrieves a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>The person model if found, otherwise null.</returns>
        Task<PersonModel?> GetPersonByIdAsync(int personId);

        /// <summary>
        /// Adds a new person asynchronously.
        /// </summary>
        /// <param name="model">The person model to add.</param>
        /// <returns>True if the person was added successfully, otherwise false.</returns>
        Task<bool> AddPersonAsync(PersonModel model);

        /// <summary>
        /// Updates an existing person asynchronously.
        /// </summary>
        /// <param name="model">The updated person model.</param>
        /// <returns>True if the person was updated successfully, otherwise false.</returns>
        Task<bool> UpdatePersonAsync(PersonModel model);

        /// <summary>
        /// Deletes a person by their ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person to delete.</param>
        /// <returns>True if the person was deleted successfully, otherwise false.</returns>
        Task<bool> DeletePersonAsync(int personId);
    }
}
