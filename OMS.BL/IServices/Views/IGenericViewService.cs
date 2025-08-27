using OMS.Common.Extensions.Pagination;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a Generic View service for retrieving T.
    /// </summary>
    public interface IGenericViewService<TModel>
    {
        /// <summary>
        /// Retrieves all Generic asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of Generic.</returns>
        Task<PagedResult<TModel>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<TModel?> GetByIdAsync(int id);
    }
}
