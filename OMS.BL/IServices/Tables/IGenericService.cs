namespace OMS.BL.IServices.Tables
{
    /// <summary>
    /// Represents a generic service interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TDto">The type of Dto.</typeparam>
    public interface IGenericService<TDto>
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Retrieves an dto by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the dto.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the dto.</returns>
        Task<TDto?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an boolean by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the dto.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> IsExistAsync(int id);

        /// <summary>
        /// Adds a new dto asynchronously.
        /// </summary>
        /// <param name="dto">The dto to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> AddAsync(TDto dto);

        /// <summary>
        /// Updates an existing dto asynchronously.
        /// </summary>
        /// <param name="dto">The dto to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> UpdateAsync(TDto dto);

        /// <summary>
        /// Deletes an dto by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the dto to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
