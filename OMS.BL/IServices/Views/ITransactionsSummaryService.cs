using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface ITransactionsSummaryService
    {
        Task<IEnumerable<TransactionsSummaryModel>> GetAllTransactionsSummaryAsync();
        Task<TransactionsSummaryModel?> GetTransactionSummaryByIdAsync(int transactionId);
    }
}
