using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsSummaryService : GenericViewService<DebtsSummary, DebtsSummaryDto>, IDebtsSummaryService
    {
        private readonly IDebtsSummaryRepository _debtsSummaryRepository;

        public DebtsSummaryService(IGenericViewRepository<DebtsSummary> genericRepo,
                                   IMapperService mapper,
                                   IDebtsSummaryRepository repository) : base(genericRepo, mapper)
        {
            _debtsSummaryRepository = repository;
        }


        /*
                 public async Task<IEnumerable<DebtsSummaryDto>> GetAllDebtsSummaryAsync()
        {
            IEnumerable<DebtsSummary> debtsSummaries = await _repository.GetAllAsync();

            return debtsSummaries?.Select(d => new DebtsSummaryDto
            {
               DebtId = d.DebtId,
               ClientName = d.ClientName,
               ServiceName = d.ServiceName,
               Description = d.Description,
               TotalDebts = d.TotalDebts,
               Status = d.Status

            }) ?? Enumerable.Empty<DebtsSummaryDto>();
        }

        public async Task<DebtsSummaryDto?> GetDebtSummaryByIdAsync(int debtId)
        {
            DebtsSummary? debt = await _repository.GetByIdAsync(debtId);

            return debt == null ? null : new DebtsSummaryDto
            {
                DebtId = debt.DebtId,
                ClientName = debt.ClientName,
                ServiceName = debt.ServiceName,
                Description = debt.Description,
                TotalDebts = debt.TotalDebts,
                Status = debt.Status
            };
        }

         */

    }
}
