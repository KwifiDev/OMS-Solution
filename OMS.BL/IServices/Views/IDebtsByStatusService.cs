using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving debts by status.
    /// </summary>
    public interface IDebtsByStatusService
    {
        /// <summary>
        /// Retrieves all debts by status asynchronously.
        /// </summary>
        /// <returns>A collection of debts by status.</returns>
        Task<IEnumerable<DebtsByStatusModel>> GetAllAsync();
    }
}
