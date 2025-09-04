using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsByStatusService : IDebtsByStatusService
    {
        private readonly IDebtsByStatusRepository _debtsByStatusRepository;
        private readonly IMapperService _mapper;

        public DebtsByStatusService(IDebtsByStatusRepository repository, IMapperService mapper)
        {
            _debtsByStatusRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DebtsByStatusModel>> GetAllAsync()
        {
            var data = await _debtsByStatusRepository.GetAllAsync();
            return data != null ? _mapper.Map<DebtsByStatus, DebtsByStatusModel>(data) : Enumerable.Empty<DebtsByStatusModel>();
        }

    }
}
