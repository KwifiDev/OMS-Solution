using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Context;
using OMS.DA.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using OMS.DA.Enums;

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

    }
}
