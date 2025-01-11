using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync();
        Task<TransactionModel?> GetTransactionByIdAsync(int transactionId);
    }
}
