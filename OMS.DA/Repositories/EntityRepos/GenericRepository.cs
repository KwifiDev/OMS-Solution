using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<PagedResult<T>> GetPagedAsync(PaginationParams parameters)
        {
            var items = await _dbSet.AsNoTracking()
                                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                    .Take(parameters.PageSize)
                                    .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = await _dbSet.CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
            };
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> IsExistAsync(int id)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var primaryKey = entityType?.FindPrimaryKey();

            if (primaryKey == null || !primaryKey.Properties.Any())
                return false;

            var primaryKeyName = primaryKey.Properties[0].Name;
            return await _dbSet.AsNoTracking().AnyAsync(e => EF.Property<int>(e, primaryKeyName) == id);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity == null) return false;

            _dbSet.Remove(entity);

            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
