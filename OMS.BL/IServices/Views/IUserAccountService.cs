using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for managing user accounts.
    /// </summary>
    public interface IUserAccountService
    {
        /// <summary>
        /// Retrieves all user accounts asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of user account models.</returns>
        Task<IEnumerable<UserAccountModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a user account by its ID asynchronously.
        /// </summary>
        /// <param name="accountId">The ID of the user account to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user account model, or null if not found.</returns>
        Task<UserAccountModel?> GetByIdAsync(int accountId);
    }
}
