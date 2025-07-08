using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.CustomAttributes;
using OMS.DA.IRepositories.IViewRepos;
using System.Reflection;

namespace OMS.DA.Repositories.ViewRepos
{
    public class GenericViewRepository<T> : IGenericViewRepository<T> where T : class
    {

        protected readonly DbSet<T> _dbSet;

        public GenericViewRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            PropertyInfo? idProperty = typeof(T).GetProperties()
                                      .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(IdAttribute)));

            if (idProperty == null)
                throw new InvalidOperationException("No property with the Id attribute found.");


            return await _dbSet
                         .AsNoTracking()
                         .Where(e => EF.Property<int>(e, idProperty.Name) == id)
                         .SingleOrDefaultAsync();
        }
    }
}
