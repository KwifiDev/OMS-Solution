using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using System.Data;

namespace OMS.DA.Repositories.EntityRepos
{
    public class DebtRepository : GenericRepository<Debt>, IDebtRepository
    {
        private readonly AppDbContext _context;

        public DebtRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EnPayDebtStatus> PayDebtByIdAsync(int debtId, string? notes, int createdByUserId)
        {
            SqlParameter returnValue = new SqlParameter
            {
                ParameterName = "@returnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_PayDebtById {debtId}, {notes}, {createdByUserId}, {returnValue} OUTPUT");

            return (EnPayDebtStatus)returnValue.Value;

        }

        public async Task<int> AddDebtAsync(int clientId, int serviceId, short quantity, string? description, string? notes, int createdByUserId)
        {
            SqlParameter newDebtId = new()
            {
                ParameterName = "@newDebtId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_AddDebt {clientId}, {serviceId}, {quantity}, {description}, {notes}, {createdByUserId}, {newDebtId} OUTPUT");

            return ((int)newDebtId.Value);
        }

    }
}
