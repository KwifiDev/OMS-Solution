using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
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

        public async Task<IEnumerable<DebtsSummaryModel>> GetAllDebtsSummaryAsync()
        {
            IEnumerable<DebtsSummary> debtsSummaries = await _repository.GetAllAsync();

            return debtsSummaries?.Select(d => new DebtsSummaryModel
            {
               DebtId = d.DebtId,
               ClientName = d.ClientName,
               ServiceName = d.ServiceName,
               Description = d.Description,
               TotalDebts = d.TotalDebts,
               Status = d.Status

            }) ?? Enumerable.Empty<DebtsSummaryModel>();
        }

        public async Task<DebtsSummaryModel?> GetDebtSummaryByIdAsync(int debtId)
        {
            DebtsSummary? debt = await _repository.GetDebtSummaryByIdAsync(debtId);

            return debt == null ? null : new DebtsSummaryModel
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
