using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class AccountService : GenericService<Account, AccountModel>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IGenericRepository<Account> genericRepo,
                              IMapperService mapper,
                              IAccountRepository accountRepository) : base(genericRepo, mapper)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountModel?> GetByClientIdAsync(int clientId)
        {
            var account = await _accountRepository.GetByClientIdAsync(clientId);

            return account != null ? _mapperService.Map<Account, AccountModel>(account) : null;
        }

        public async Task<bool> DepositIntoAccountAsync(AccountTransactionModel model)
        {
            model.TransactionStatus = await _accountRepository.DepositAccountAsync
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
            model.TransactionStatus = await _accountRepository.WithdrawAccountAsync
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
