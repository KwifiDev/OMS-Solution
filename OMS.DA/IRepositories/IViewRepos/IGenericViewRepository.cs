using OMS.Common.Extensions.Pagination;

namespace OMS.DA.IRepositories.IViewRepos
{
    /// <summary>
    /// Represents a generic repository interface for Retrieve Data operations.
    /// </summary>
    /// <typeparam name="TView">The entity type.</typeparam>
    public interface IGenericViewRepository<TView> where TView : class
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<PagedResult<TView>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or null if not found.</returns>
        Task<TView?> GetByIdAsync(int id);
    }
}
