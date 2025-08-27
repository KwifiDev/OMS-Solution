using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IClientsByTypeRepository
    {
        /// <summary>
        /// Retrieves all ClientsByType.
        /// </summary>
        /// <returns>The task result contains the collection of ClientsByType.</returns>
        Task<PagedResult<ClientsByType>> GetPagedAsync(PaginationParams parameters);
    }
}
