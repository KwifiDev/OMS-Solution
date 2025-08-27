using OMS.BL.Models.Views;
using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving clients grouped by type.
    /// </summary>
    public interface IClientsByTypeService
    {
        /// <summary>
        /// Retrieves all clients grouped by type asynchronously.
        /// </summary>
        /// <returns>A collection of clients grouped by type.</returns>
        Task<PagedResult<ClientsByTypeModel>> GetPagedAsync(PaginationParams parameters);
    }
}
