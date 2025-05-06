using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class RevenueService : GenericService<Revenue, RevenueModel>, IRevenueService
    {
        private readonly IRevenueRepository _revenueRepository;

        public RevenueService(IGenericRepository<Revenue> genericRepo,
                              IMapperService mapper,
                              IRevenueRepository repository) : base(genericRepo, mapper)
        {
            _revenueRepository = repository;
        }

    }
}
