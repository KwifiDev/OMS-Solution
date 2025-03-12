using OMS.BL.Dtos.Views;

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
        Task<IEnumerable<PersonDetailDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a person detail by ID asynchronously.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the person detail model, or null if not found.</returns>
        Task<PersonDetailDto?> GetByIdAsync(int personId);
    }
}
