using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.DA.IRepositories.IViewRepos
{
    public interface IPaymentsSummaryRepository : IGenericViewRepository<PaymentsSummary>
    {
        /// <summary>
        /// Retrieves an PaymentsSummary by PaymentId.
        /// </summary>
        /// <param name="paymentId">The ID of the payment.</param>
        /// <returns>The PaymentsSummary if found; otherwise, null.</returns>
        Task<PaymentsSummary?> GetPaymentSummaryByIdAsync(int paymentId);
    }
}
