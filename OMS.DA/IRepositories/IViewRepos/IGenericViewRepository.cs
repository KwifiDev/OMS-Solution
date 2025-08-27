using OMS.Common.Extensions.Pagination;

namespace OMS.DA.IRepositories.IViewRepos
{
    /// <summary>
    /// Represents a generic repository interface for Retrieve Data operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IGenericViewRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<PagedResult<T>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or null if not found.</returns>
        Task<T?> GetByIdAsync(int id);
    }
}
