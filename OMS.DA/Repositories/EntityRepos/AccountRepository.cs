﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using System.Data;

namespace OMS.DA.Repositories.EntityRepos
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Account?> GetByClientIdAsync(int clientId)
        {
            return await _context.Accounts.Where(a => a.ClientId == clientId).FirstOrDefaultAsync();
        }

        public async Task<EnAccountTransactionStatus> DepositAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId)
        {
            SqlParameter returnValue = new SqlParameter
            {
                ParameterName = "@returnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_DepositByAccountID {accountId}, {amount}, {notes}, {createdByUserId}, {returnValue} OUTPUT");

            return (EnAccountTransactionStatus)returnValue.Value;
        }

        public async Task<EnAccountTransactionStatus> WithdrawAccountAsync(int accountId, decimal amount, string? notes, int createdByUserId)
        {
            SqlParameter returnValue = new SqlParameter
            {
                ParameterName = "@returnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_WithdrawByAccountID {accountId}, {amount}, {notes}, {createdByUserId}, {returnValue} OUTPUT");

            return (EnAccountTransactionStatus)returnValue.Value;
        }
    }
}
