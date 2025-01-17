using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsByStatusService : GenericViewService<DebtsByStatus, DebtsByStatusDto>, IDebtsByStatusService
    {
        private readonly IDebtsByStatusRepository _debtsByStatusRepository;

        public DebtsByStatusService(IGenericViewRepository<DebtsByStatus> genericRepo,
                                    IMapperService mapper,
                                    IDebtsByStatusRepository repository) : base(genericRepo, mapper)
        {
            _debtsByStatusRepository = repository;
        }


        /*
                 public async Task<IEnumerable<DebtsByStatusDto>> GetAllDebtsByStatusAsync()
        {
            IEnumerable<DebtsByStatus> debtsByStatus = await _repository.GetAllAsync();

            return debtsByStatus?.Select(d => new DebtsByStatusDto
            {
                DebtsStatus = d.DebtsStatus,
                TotalDebts = d.TotalDebts,
                TotalAmount = d.TotalAmount

            }) ?? Enumerable.Empty<DebtsByStatusDto>();
        }
         */

    }
}
