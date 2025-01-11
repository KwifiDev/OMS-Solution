using OMS.DA.Entities;
using OMS.DA.Enums;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<EnAccountTransactionStatus> DepositAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId);
        Task<EnAccountTransactionStatus> WithdrawAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId);
    }
}
