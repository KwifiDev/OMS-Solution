using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a service for managing user data.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of user models.</returns>
        Task<IEnumerable<UserModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a user by their ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user dto, or null if not found.</returns>
        Task<UserModel?> GetByIdAsync(int userId);

        /// <summary>
        /// find a user by their ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to find.</param>
        /// <returns>True if the user was exist, otherwise false.</returns>
        Task<bool> IsExistAsync(int userId);

        /// <summary>
        /// Retrieves a user by their person ID asynchronously.
        /// </summary>
        /// <param name="personId">The person ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user dto, or null if not found.</returns>
        Task<UserModel?> GetByPersonIdAsync(int personId);

        /// <summary>
        /// Retrieves a user ID by their person ID asynchronously.
        /// </summary>
        /// <param name="personId">The person ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user ID, or null if not found.</returns>
        Task<int> GetIdByPersonIdAsync(int personId);

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="dto">The user dto to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was added successfully.</returns>
        Task<bool> AddAsync(UserModel dto);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="dto">The user dto to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was updated successfully.</returns>
        Task<bool> UpdateAsync(UserModel dto);

        /// <summary>
        /// Deletes a user by their ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was deleted successfully.</returns>
        Task<bool> DeleteAsync(int userId);

        /// <summary>
        /// Retrieves a user by their username and password asynchronously.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user dto, or null if not found.</returns>
        Task<UserLoginModel?> GetByUsernameAndPasswordAsync(string username, string password);


        /// <summary>
        /// Retrieves a user by their person Id asynchronously.
        /// </summary>
        /// <param name="personId">The person Id of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user dto, or null if not found.</returns>
        Task<UserLoginModel?> GetUserLoginByPersonIdAsync(int personId);


    }
}
