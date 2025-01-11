using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDiscountsAppliedRepository : IGenericViewRepository<DiscountsApplied>
    {
        /// <summary>
        /// Retrieves an DiscountsApplied by DiscountId.
        /// </summary>
        /// <param name="discountId">The ID of the discount.</param>
        /// <returns>The DiscountsApplied if found; otherwise, null.</returns>
        Task<DiscountsApplied?> GetDiscountAppliedByIdAsync(int discountId);
    }
}
