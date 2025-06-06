﻿namespace OMS.DA.IRepositories.IEntityRepos
{
    /// <summary>
    /// Represents a generic repository interface for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or null if not found.</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an boolean by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the boolean value.</returns>
        Task<bool> IsExistAsync(int id);

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity was added successfully.</returns>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity was updated successfully.</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the entity was deleted successfully.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
