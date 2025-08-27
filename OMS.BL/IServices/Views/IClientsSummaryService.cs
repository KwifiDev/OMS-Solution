using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving client Summary.
    /// </summary>
    public interface IClientsSummaryService
    {
        /// <summary>
        /// Retrieves all client Summary asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of client Summary models.</returns>
        Task<PagedResult<ClientsSummaryModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a client Summary by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client Summary model, or null if not found.</returns>
        Task<ClientsSummaryModel?> GetByIdAsync(int clientId);
    }
}
