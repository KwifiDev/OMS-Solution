using OMS.Common.Extensions.Pagination;

namespace OMS.UI.APIs.Services.Interfaces.Tables
{
    /// <summary>
    /// Represents a generic service interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TDto">The type of dto.</typeparam>
    /// <typeparam name="TModel">The type of model.</typeparam>
    public interface IGenericApiService<TDto, TModel>
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<PagedResult<TModel>?> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an model by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the model.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the model.</returns>
        Task<TModel?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an boolean by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the model.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> IsExistAsync(int id);

        /// <summary>
        /// Adds a new model asynchronously.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> AddAsync(TModel model);

        /// <summary>
        /// Updates an existing model asynchronously.
        /// </summary>
        /// <param name="id">The model id to update.</param>
        /// <param name="model">The model to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> UpdateAsync(int id, TModel model);

        /// <summary>
        /// Deletes an model by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the model to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
