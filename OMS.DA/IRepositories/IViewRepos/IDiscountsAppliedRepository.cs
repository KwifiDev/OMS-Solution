using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IDiscountsAppliedRepository
    {
        /// <summary>
        /// Retrieves all DiscountsApplied.
        /// </summary>
        /// <returns>The task result contains the collection of DiscountsApplied.</returns>
        Task<IEnumerable<DiscountsApplied>> GetAllAsync();

        /// <summary>
        /// Retrieves an DiscountsApplied by serviceId.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>The DiscountsApplied if found; otherwise, null.</returns>
        Task<IEnumerable<DiscountsApplied>> GetByServiceIdAsync(int serviceId);

        /// <summary>
        /// Retrieves an DiscountsApplied by DiscountId.
        /// </summary>
        /// <param name="discountId">The ID of the discount.</param>
        /// <returns>The DiscountsApplied if found; otherwise, null.</returns>
        Task<DiscountsApplied?> GetByIdAsync(int discountId);
    }
}
