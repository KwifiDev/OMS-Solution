using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IAccountBalancesTransactionService
    {
        Task<IEnumerable<AccountBalancesTransactionModel>> GetAllAccountsBalancesTransactionsAsync();
        Task<AccountBalancesTransactionModel?> GetAccountBalancesTransactionByIdAsync(int accountId);
    }
}
