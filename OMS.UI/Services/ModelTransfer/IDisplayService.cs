namespace OMS.UI.Services.ModelTransfer
{
    public interface IDisplayService<TModel>
    {
        /// <summary>
        /// Retrieves all asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of Generic.</returns>
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<TModel?> GetByIdAsync(int id);
    }
}
