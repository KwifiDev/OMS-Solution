
using OMS.UI.Models;

namespace OMS.UI.APIs.Services.Interfaces.Views
{
    /// <summary>
    /// Represents a service for retrieving user details.
    /// </summary>
    public interface IUserDetailService
    {
        /// <summary>
        /// Retrieves all user details asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of user detail models.</returns>
        Task<IEnumerable<UserDetailModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a user detail by ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user detail model, or null if not found.</returns>
        Task<UserDetailModel?> GetByIdAsync(int userId);
    }
}
