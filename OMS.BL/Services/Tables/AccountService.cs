using OMS.BL.IServices.Tables;
using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.Enums;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountModel>> GetAllAccountsAsync()
        {
            IEnumerable<Account> accounts = await _repository.GetAllAsync();

            return accounts?.Select(a => new AccountModel
            {
                AccountId = a.AccountId,
                ClientId = a.ClientId,
                Balance = a.Balance,
                UserAccount = a.UserAccount

            }) ?? Enumerable.Empty<AccountModel>();
        }

        public async Task<AccountModel?> GetAccountByIdAsync(int accountId)
        {
            Account? account = await _repository.GetByIdAsync(accountId);

            return account == null ? null : new AccountModel
            {
                AccountId = account.AccountId,
                ClientId = account.ClientId,
                Balance = account.Balance,
                UserAccount = account.UserAccount
            };
        }

        public async Task<bool> AddAccountAsync(AccountModel model)
        {
            if (model == null) return false;

            Account account = new Account
            {
                ClientId = model.ClientId,
                Balance = 0, // Default Value
                UserAccount = model.UserAccount
            };

            bool success = await _repository.AddAsync(account);

            if (success) model.AccountId = account.AccountId;

            return success;
        }

        public async Task<bool> UpdateAccountAsync(AccountModel model)
        {
            if (model == null) return false;

            Account? account = await _repository.GetByIdAsync(model.AccountId);

            if (account == null) return false;

            account.UserAccount = model.UserAccount;

            return await _repository.UpdateAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            if (accountId <= 0) return false;

            return await _repository.DeleteAsync(accountId);
        }

        public async Task<bool> DepositIntoAccountAsync(AccountTransactionModel model)
        {
            model.TransactionStatus = await _repository.DepositAccountAsync
               (
                   accountId: model.AccountId,
                   amount: model.Amount,
                   notes: model.Notes,
                   createdByUserId: model.CreatedByUserId
               );

            return model.TransactionStatus == EnAccountTransactionStatus.Success;
        }

        public async Task<bool> WithdrawFromAccountAsync(AccountTransactionModel model)
        {
            model.TransactionStatus = await _repository.WithdrawAccountAsync
                (
                    accountId: model.AccountId,
                    amount: model.Amount,
                    notes: model.Notes,
                    createdByUserId: model.CreatedByUserId
                );

            return model.TransactionStatus == EnAccountTransactionStatus.Success;
        }
    }
}
