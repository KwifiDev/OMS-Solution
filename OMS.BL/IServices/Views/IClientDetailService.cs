using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving client details.
    /// </summary>
    public interface IClientDetailService
    {
        /// <summary>
        /// Retrieves all client details asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of client detail models.</returns>
        Task<PagedResult<ClientDetailModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves a client detail by ID asynchronously.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the client detail model, or null if not found.</returns>
        Task<ClientDetailModel?> GetByIdAsync(int clientId);
    }
}
