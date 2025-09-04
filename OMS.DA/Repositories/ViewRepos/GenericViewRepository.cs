using Microsoft.EntityFrameworkCore;
using OMS.Common.Extensions.Pagination;
using OMS.DA.Context;
using OMS.DA.Interfaces;
using OMS.DA.IRepositories.IViewRepos;

namespace OMS.DA.Repositories.ViewRepos
{
    public class GenericViewRepository<TView> : IGenericViewRepository<TView> where TView : class, IEntityKey
    {

        protected readonly DbSet<TView> _dbSet;

        public GenericViewRepository(AppDbContext context)
        {
            _dbSet = context.Set<TView>();
        }

        public virtual async Task<PagedResult<TView>> GetPagedAsync(PaginationParams parameters)
        {
            var items = await _dbSet.AsNoTracking()
                                    .OrderByDescending(e => e.Id)
                                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                    .Take(parameters.PageSize)
                                    .ToListAsync();

            return new PagedResult<TView>
            {
                Items = items,
                TotalItems = await _dbSet.CountAsync(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
            };
        }

        public virtual async Task<TView?> GetByIdAsync(int id)
        {
            return await _dbSet
                         .AsNoTracking()
                         .Where(e => e.Id == id)
                         .SingleOrDefaultAsync();
        }
    }
}
