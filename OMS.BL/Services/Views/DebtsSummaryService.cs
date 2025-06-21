using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsSummaryService : GenericViewService<DebtsSummary, DebtsSummaryModel>, IDebtsSummaryService
    {
        private readonly IDebtsSummaryRepository _debtsSummaryRepository;

        public DebtsSummaryService(IGenericViewRepository<DebtsSummary> genericRepo,
                                   IMapperService mapper,
                                   IDebtsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _debtsSummaryRepository = repository;
        }

        public async Task<IEnumerable<DebtsSummaryModel>> GetByClientIdAsync(int clientId)
        {
            var debtsSummary = await _debtsSummaryRepository.GetByClientIdAsync(clientId);

            return _mapper.Map<IEnumerable<DebtsSummary>, IEnumerable<DebtsSummaryModel>>(debtsSummary);
        }
    }
}
