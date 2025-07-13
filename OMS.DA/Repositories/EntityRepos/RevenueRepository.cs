using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class RevenueRepository : GenericRepository<Revenue>, IRevenueRepository
    {
        public RevenueRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<DateOnly> GetLastAddRevenueDate()
            => await _dbSet.Select(r => r.CreatedAt).OrderDescending().FirstOrDefaultAsync();
        
    }
}
