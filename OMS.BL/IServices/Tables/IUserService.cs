﻿using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;

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
        /// <param name="model">The user model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user was updated successfully.</returns>
        Task<bool> UpdateAsync(UserModel model);

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
        /// Is User Active by their ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to find if active.</param>
        /// <returns>True if the user was active, false if user not active, otherwise false if not found.</returns>
        Task<bool> IsUserActive(int userId);


        /// <summary>
        /// Updates an existing user activation asynchronously.
        /// </summary>
        /// <param name="userId">The user id to update activation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the user activation was updated successfully.</returns>
        Task<bool> UpdateUserActivationStatus(int userId, bool isActive);


        /// <summary>
        /// Is UserName used asynchronously.
        /// </summary>
        /// <param name="userId">The user id to not check the selected user.</param>
        /// <param name="username">The username of the user to find if used.</param>
        /// <returns>True if the username used, otherwise false if not found.</returns>
        Task<bool> IsUsernameUsedAsync(int userId, string username);

        /// <summary>
        /// get the UserName asynchronously.
        /// </summary>
        /// <param name="userId">The Id of the user to find if username.</param>
        /// <returns>username if the user exist, otherwise null if not found.</returns>
        Task<string?> GetUsernameById(int userId);


        /// <summary>
        /// Check if UserName can be saved asynchronously.
        /// </summary>
        /// <param name="model">The Model of the user to Check UserName if valid.</param>
        /// <returns>true if the username valid, null if user not found, false if username confilt with onther user.</returns>
        Task<bool?> IsUsernameValid(UserModel model);


        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="model">The user model to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result of User Save value indicating whether the user was updated successfully.</returns>
        Task<EnUserResult> UpdateUserAsync(UserModel model);
    }
}
