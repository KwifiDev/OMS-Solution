using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
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


        public async Task<bool> StartTransactionAsync(AccountTransactionModel model)
        {
            return model.TransactionType switch
            {
                EnTransactionType.Deposit => await DepositIntoAccountAsync(model),
                EnTransactionType.Withdraw => await WithdrawFromAccountAsync(model),
                EnTransactionType.Transfer => false,
                _ => false,
            };
        }

        private async Task<bool> DepositIntoAccountAsync(AccountTransactionModel model)
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

        private async Task<bool> WithdrawFromAccountAsync(AccountTransactionModel model)
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
