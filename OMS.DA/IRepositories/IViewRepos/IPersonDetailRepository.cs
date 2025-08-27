using OMS.Common.Extensions.Pagination;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IPersonDetailRepository
    {
        /// <summary>
        /// Retrieves all PersonDetail.
        /// </summary>
        /// <returns>The task result contains the collection of PersonDetail.</returns>
        Task<PagedResult<PersonDetail>> GetPagedAsync(PaginationParams parameters);

        /// <summary>
        /// Retrieves an PersonDetail by PersonId.
        /// </summary>
        /// <param name="personId">The ID of the Person.</param>
        /// <returns>The PersonDetail if found; otherwise, null.</returns>
        Task<PersonDetail?> GetByIdAsync(int personId);
    }
}
