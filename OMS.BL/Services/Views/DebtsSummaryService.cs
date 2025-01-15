using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DebtsSummaryService : IDebtsSummaryService
    {
        private readonly IDebtsSummaryRepository _repository;

        public DebtsSummaryService(IDebtsSummaryRepository repository)
        {
            _repository = repository;
        }

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

    }
}
