using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsByStatusService : IDebtsByStatusService
    {
        private readonly IDebtsByStatusRepository _repository;

        public DebtsByStatusService(IDebtsByStatusRepository repository)
        {
            _repository = repository;
        }

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

    }
}
