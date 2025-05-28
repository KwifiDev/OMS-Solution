using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving Service Summary.
    /// </summary>
    public interface IServicesSummaryService
    {
        /// <summary>
        /// Retrieves all Service Summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Service Summary models.</returns>
        Task<IEnumerable<ServicesSummaryModel>> GetAllAsync();

        /// <summary>
        /// Retrieves a Service Summary by ID asynchronously.
        /// </summary>
        /// <param name="serviceId">The ID of the Service.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Service Summary model, or null if not found.</returns>
        Task<ServicesSummaryModel?> GetByIdAsync(int serviceId);
    }
}
