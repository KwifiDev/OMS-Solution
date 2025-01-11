using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface ISalesSummaryRepository : IGenericViewRepository<SalesSummary>
    {
        /// <summary>
        /// Retrieves an SalesSummary by SaleId.
        /// </summary>
        /// <param name="saleId">The ID of the Sale.</param>
        /// <returns>The SalesSummary if found; otherwise, null.</returns>
        Task<SalesSummary?> GetSaleSummaryByIdAsync(int saleId);
    }
}
