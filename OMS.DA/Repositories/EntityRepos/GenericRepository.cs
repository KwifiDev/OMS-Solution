using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.Interfaces;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntityKey
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<PagedResult<TEntity>> GetPagedAsync(PaginationParams parameters)
        {
            var items = await _dbSet.AsNoTracking()
                                    .OrderByDescending(e => e.Id)
                                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                    .Take(parameters.PageSize)
                                    .ToListAsync();

            return new PagedResult<TEntity>
            {
                Items = items,
                TotalItems = await _dbSet.CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
            };
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> IsExistAsync(int id)
        {
            return await _dbSet.AsNoTracking().AnyAsync(e => e.Id == id);
        }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);

            if (entity == null) return false;

            _dbSet.Remove(entity);

            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
