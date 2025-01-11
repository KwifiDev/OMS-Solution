using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.ViewRepos
{
    public class GenericViewRepository<T> : IGenericViewRepository<T> where T : class
    {

        private readonly DbSet<T> _dbSet;

        public GenericViewRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
