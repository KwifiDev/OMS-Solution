using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using System.Data;

namespace OMS.DA.Repositories.EntityRepos
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<EnPayDebtStatus> PayAllDebtsByIdAsync(int clientId, string? notes, int createdByUserId)
        {
            SqlParameter returnValue = new SqlParameter
            {
                ParameterName = "@returnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_PayAllDebtsByClientId {clientId}, {notes}, {createdByUserId}, {returnValue} OUTPUT");

            return (EnPayDebtStatus)returnValue.Value;
        }

        public async Task<Client?> GetByPersonIdAsync(int personId)
        {
            return await _context.Clients.Where(c => c.PersonId == personId).FirstOrDefaultAsync();
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _context.Clients.Where(c => c.PersonId == personId).Select(c => c.Id).FirstOrDefaultAsync();
        }
    }
}
