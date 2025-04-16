using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class AccountService : GenericService<Account, AccountDto>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IGenericRepository<Account> genericRepo,
                              IMapperService mapper,
                              IAccountRepository accountRepository) : base(genericRepo, mapper)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto?> GetByClientIdAsync(int clientId)
        {
            var account = await _accountRepository.GetByClientIdAsync(clientId);

            return account != null ? _mapperService.Map<Account, AccountDto>(account) : null;
        }

        public async Task<bool> DepositIntoAccountAsync(AccountTransactionDto dto)
        {
            dto.TransactionStatus = await _accountRepository.DepositAccountAsync
               (
                   accountId: dto.AccountId,
                   amount: dto.Amount,
                   notes: dto.Notes,
                   createdByUserId: dto.CreatedByUserId
               );

            return dto.TransactionStatus == EnAccountTransactionStatus.Success;
        }

        public async Task<bool> WithdrawFromAccountAsync(AccountTransactionDto dto)
        {
            dto.TransactionStatus = await _accountRepository.WithdrawAccountAsync
                (
                    accountId: dto.AccountId,
                    amount: dto.Amount,
                    notes: dto.Notes,
                    createdByUserId: dto.CreatedByUserId
                );

            return dto.TransactionStatus == EnAccountTransactionStatus.Success;
        }

        /*
         
          public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            IEnumerable<Account> accounts = await _repository.GetAllAsync();

            return accounts?.Select(a => new AccountDto
            {
                AccountId = a.AccountId,
                ClientId = a.ClientId,
                Balance = a.Balance,
                UserAccount = a.UserAccount

            }) ?? Enumerable.Empty<AccountDto>();
        }

        public async Task<AccountDto?> GetAccountByIdAsync(int accountId)
        {
            Account? account = await _repository.GetByIdAsync(accountId);

            return account == null ? null : new AccountDto
            {
                AccountId = account.AccountId,
                ClientId = account.ClientId,
                Balance = account.Balance,
                UserAccount = account.UserAccount
            };
        }

        public async Task<bool> AddAccountAsync(AccountDto dto)
        {
            if (dto == null) return false;

            Account account = new Account
            {
                ClientId = dto.ClientId,
                Balance = 0, // Default Value
                UserAccount = dto.UserAccount
            };

            bool success = await _repository.AddAsync(account);

            if (success) dto.AccountId = account.AccountId;

            return success;
        }

        public async Task<bool> UpdateAccountAsync(AccountDto dto)
        {
            if (dto == null) return false;

            Account? account = await _repository.GetByIdAsync(dto.AccountId);

            if (account == null) return false;

            account.UserAccount = dto.UserAccount;

            return await _repository.UpdateAsync(account);
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            if (accountId <= 0) return false;

            return await _repository.DeleteAsync(accountId);
        }
          
          
         */
    }
}
