using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> IsDiscountAlreadyApplied(int serviceId, EnClientType clientType)
            => await _dbSet.AnyAsync(d => d.ServiceId == serviceId && d.ClientType == clientType);
    }
}
