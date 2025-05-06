using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    /// <summary>
    /// Represents a service for retrieving transactions by type.
    /// </summary>
    public interface ITransactionsByTypeService
    {
        /// <summary>
        /// Retrieves all transactions grouped by type asynchronously.
        /// </summary>
        /// <returns>A collection of transactions grouped by type.</returns>
        Task<IEnumerable<TransactionsByTypeModel>> GetAllAsync();
    }
}
