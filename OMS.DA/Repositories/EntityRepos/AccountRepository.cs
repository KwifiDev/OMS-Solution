using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using OMS.DA.Enums;

namespace OMS.DA.Repositories.EntityRepos
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
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
