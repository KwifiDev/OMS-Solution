using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IServicesSummaryRepository
    {
        /// <summary>
        /// Retrieves all ServicesSummary
        /// </summary>
        /// <returns>The task result contains the collection of ServicesSummary.</returns>
        Task<PagedResult<ServicesSummary>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an ServicesSummary by ServiceId.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>The ServicesSummary if found; otherwise, null.</returns>
        Task<ServicesSummary?> GetByIdAsync(int serviceId);
    }
}
