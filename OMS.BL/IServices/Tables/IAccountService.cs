using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountModel>> GetAllAccountsAsync();
        Task<AccountModel?> GetAccountByIdAsync(int accountId);
        Task<bool> AddAccountAsync(AccountModel model);
        Task<bool> UpdateAccountAsync(AccountModel model);
        Task<bool> DeleteAccountAsync(int accountId);
        Task<bool> DepositIntoAccountAsync(AccountTransactionModel model);
        Task<bool> WithdrawFromAccountAsync(AccountTransactionModel model);
    }
}
