using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using System.Data;

namespace OMS.DA.Repositories.EntityRepos
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CreateNewSaleAsync(int clientId, int serviceId, short quantity, string? description, string? notes, EnSaleStatus status, int createdByUserId)
        {
            SqlParameter newSaleId = new()
            {
                ParameterName = "@newSaleId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlInterpolatedAsync
                ($"EXEC dbo.SP_AddSale {clientId}, {serviceId}, {quantity}, {description}, {notes}, {status}, {createdByUserId}, {newSaleId} OUTPUT");

            return ((int)newSaleId.Value);
        }
    }
}
