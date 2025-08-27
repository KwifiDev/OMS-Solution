using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IClientDetailRepository
    {
        /// <summary>
        /// Retrieves all ClientDetail.
        /// </summary>
        /// <returns>The task result contains the collection of ClientDetail.</returns>
        Task<PagedResult<ClientDetail>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an ClientDetail by ClientId.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The ClientDetail if found; otherwise, null.</returns>
        Task<ClientDetail?> GetByIdAsync(int clientId);
    }
}
