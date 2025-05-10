using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class PermissionsConfigService : GenericService<PermissionsConfig, PermissionsConfigModel>, IPermissionsConfigService
    {
        private readonly IPermissionsConfigRepository _permissionsConfigRepository;

        public PermissionsConfigService(IGenericRepository<PermissionsConfig> genericRepo,
                                        IMapperService mapper,
                                        IPermissionsConfigRepository repository) : base(genericRepo, mapper)
        {
            _permissionsConfigRepository = repository;
        }

        public override Task<bool> AddAsync(PermissionsConfigModel model)
           => throw new NotSupportedException("Add operation is not supported for PermissionsConfigService.");
        public override Task<bool> UpdateAsync(PermissionsConfigModel model)
            => throw new NotSupportedException("Update operation is not supported for PermissionsConfigService.");
        public override Task<bool> DeleteAsync(int id)
            => throw new NotSupportedException("Delete operation is not supported for PermissionsConfigService.");


    }
}
