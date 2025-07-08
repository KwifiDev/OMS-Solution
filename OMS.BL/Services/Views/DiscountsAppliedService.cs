using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DiscountsAppliedService : GenericViewService<DiscountsApplied, DiscountsAppliedModel>, IDiscountsAppliedService
    {
        private readonly IDiscountsAppliedRepository _discountsAppliedRepository;

        public DiscountsAppliedService(IGenericViewRepository<DiscountsApplied> genericRepo,
                                       IMapperService mapper,
                                       IDiscountsAppliedRepository repository) : base(genericRepo, mapper)
        {
            _discountsAppliedRepository = repository;
        }

        public async Task<IEnumerable<DiscountsAppliedModel>> GetByServiceIdAsync(int serviceId)
        {
            var discountsApplied = await _discountsAppliedRepository.GetByServiceIdAsync(serviceId);

            return _mapper.Map<IEnumerable<DiscountsApplied>, IEnumerable<DiscountsAppliedModel>>(discountsApplied);
        }
    }
}
