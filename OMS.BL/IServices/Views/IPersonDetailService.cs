using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving person details.
    /// </summary>
    public interface IPersonDetailService
    {
        /// <summary>
        /// Retrieves all person details asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of person detail models.</returns>
        Task<IEnumerable<PersonDetailModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a person detail by ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the person detail model, or null if not found.</returns>
        Task<PersonDetailModel?> GetByIdAsync(int personId);
    }
}
