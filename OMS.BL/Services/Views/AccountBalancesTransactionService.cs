﻿using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class AccountBalancesTransactionService : IAccountBalancesTransactionService
    {
        private readonly IAccountBalancesTransactionRepository _repository;

        public AccountBalancesTransactionService(IAccountBalancesTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountBalancesTransactionModel>> GetAllAccountsBalancesTransactionsAsync()
        {
            IEnumerable<AccountBalancesTransaction> accountBalancesTransaction = await _repository.GetAllAsync();

            return accountBalancesTransaction?.Select(a => new AccountBalancesTransactionModel
            {
                AccountId = a.AccountId,
                ClientName = a.ClientName,
                UserAccount = a.UserAccount,
                AccountBalance = a.AccountBalance,
                TotalTransactions = a.TotalTransactions,
                TotalTransactionAmount = a.TotalTransactionAmount

            }) ?? Enumerable.Empty<AccountBalancesTransactionModel>();
        }

        public async Task<AccountBalancesTransactionModel?> GetAccountBalancesTransactionByIdAsync(int accountId)
        {
            AccountBalancesTransaction? account = await _repository.GetAccountBalancesTransactionByIdAsync(accountId);

            return account == null ? null : new AccountBalancesTransactionModel
            {
                AccountId = account.AccountId,
                ClientName = account.ClientName,
                UserAccount = account.UserAccount,
                AccountBalance = account.AccountBalance,
                TotalTransactions = account.TotalTransactions,
                TotalTransactionAmount = account.TotalTransactionAmount
            };
        }

    }
}
