using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.DA.Repositories.ViewRepos
{
    public class ClientsByTypeRepository : IClientsByTypeRepository
    {
        private readonly DbSet<ClientsByType> _dbSet;

        public ClientsByTypeRepository(AppDbContext context)
        {
            _dbSet = context.Set<ClientsByType>();
        }

        public async Task<IEnumerable<ClientsByType>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
