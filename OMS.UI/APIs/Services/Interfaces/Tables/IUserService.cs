using OMS.Common.Extensions.Pagination;
using OMS.UI.Models.Others;
using OMS.UI.Models.Tables;

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
        Task<PagedResult<UserModel>?> GetPagedAsync(PaginationParams parameters);

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
        /// Retrieves a user by their person Id asynchronously.
        /// </summary>
        /// <param name="personId">The person Id of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user model, or null if not found.</returns>
        Task<UserLoginModel?> GetUserLoginByPersonIdAsync(int personId);


        /// <summary>
        /// Updates an existing user activation asynchronously.
        /// </summary>
        /// <param name="userId">The id of the user to update activation.</param>
        /// <param name="isActive">The bool value of the user activation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user id, or null if not found.</returns>
        Task<bool> UpdateUserActivationStatus(int userId, bool isActive);

        /// <summary>
        /// Check if username Available asynchronously.
        /// </summary>
        /// <param name="userId">The id of the user to update activation.</param>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>True if the username Available, otherwise false.</returns>
        Task<bool> CheckUsernameAvailable(int userId, string username);


        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="id">The user model id to update.</param>
        /// <param name="model">The user model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was updated successfully.</returns>
        Task<bool> UpdateMyUserAsync(int id, UserModel model);


    }
}
