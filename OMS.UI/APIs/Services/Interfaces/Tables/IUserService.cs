
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Tables
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
        /// <returns>A task that represents the asynchronous operation. The task result contains the user model, or null if not found.</returns>
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
        /// <returns>A task that represents the asynchronous operation. The task result contains the user model, or null if not found.</returns>
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
        /// <param name="model">The user model to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was added successfully.</returns>
        Task<bool> AddAsync(UserModel model);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="id">The user model id to update.</param>
        /// <param name="model">The user model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was updated successfully.</returns>
        Task<bool> UpdateAsync(int id, UserModel model);

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
        /// <returns>A task that represents the asynchronous operation. The task result contains the user model, or null if not found.</returns>
        Task<UserLoginModel?> GetByUsernameAndPasswordAsync(string username, string password);


        /// <summary>
        /// Retrieves a user by their person Id asynchronously.
        /// </summary>
        /// <param name="personId">The person Id of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user model, or null if not found.</returns>
        Task<UserLoginModel?> GetUserLoginByPersonIdAsync(int personId);


    }
}
