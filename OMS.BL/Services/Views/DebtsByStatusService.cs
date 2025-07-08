using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsByStatusService : GenericViewService<DebtsByStatus, DebtsByStatusModel>, IDebtsByStatusService
    {
        private readonly IDebtsByStatusRepository _debtsByStatusRepository;

        public DebtsByStatusService(IGenericViewRepository<DebtsByStatus> genericRepo,
                                    IMapperService mapper,
                                    IDebtsByStatusRepository repository) : base(genericRepo, mapper)
        {
            _debtsByStatusRepository = repository;
        }

    }
}
