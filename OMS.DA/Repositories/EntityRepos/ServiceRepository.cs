using Microsoft.EntityFrameworkCore;
using OMS.DA.Context;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.DA.Repositories.EntityRepos
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DbSet<Service> _services;

        public ServiceRepository(AppDbContext context) : base(context)
        {
            _services = context.Set<Service>();
        }

        

        public async Task<List<Service>> GetAllServicesOption()
        {
            return await _services.Select(s => new Service { ServiceId = s.ServiceId, Name = s.Name }).ToListAsync();
        }
    }
}
