using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class MonthlyFinancialSummaryService : GenericViewService<MonthlyFinancialSummary, MonthlyFinancialSummaryDto>, IMonthlyFinancialSummaryService
    {
        private readonly IMonthlyFinancialSummaryRepository _monthlyFinancialSummaryRepository;

        public MonthlyFinancialSummaryService(IGenericViewRepository<MonthlyFinancialSummary> genericRepo,
                                              IMapperService mapper,
                                              IMonthlyFinancialSummaryRepository repository) : base(genericRepo, mapper)
        {
            _monthlyFinancialSummaryRepository = repository;
        }


        /*
         
        public async Task<IEnumerable<MonthlyFinancialSummaryDto>> GetAllMonthlyFinancialSummariesAsync()
        {
            IEnumerable<MonthlyFinancialSummary> monthlyFinancialSummary = await _monthlyFinancialSummaryRepository.GetAllAsync();

            return monthlyFinancialSummary?.Select(m => new MonthlyFinancialSummaryDto
            {
                Year = m.Year,
                Month = m.Month,
                TotalRevenue = m.TotalRevenue

            }) ?? Enumerable.Empty<MonthlyFinancialSummaryDto>();
        }
         */

    }
}
