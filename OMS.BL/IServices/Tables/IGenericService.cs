namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a generic service interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TDto">The type of Dto.</typeparam>
    /// <typeparam name="TEntity">The type of Entity.</typeparam>
    public interface IGenericService<TDto>
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<TDto?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> AddAsync(TDto entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> UpdateAsync(TDto entity);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
