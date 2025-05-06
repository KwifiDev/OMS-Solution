using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class MonthlyFinancialSummaryService : GenericViewService<MonthlyFinancialSummary, MonthlyFinancialSummaryModel>, IMonthlyFinancialSummaryService
    {
        private readonly IMonthlyFinancialSummaryRepository _monthlyFinancialSummaryRepository;

        public MonthlyFinancialSummaryService(IGenericViewRepository<MonthlyFinancialSummary> genericRepo,
                                              IMapperService mapper,
                                              IMonthlyFinancialSummaryRepository repository) : base(genericRepo, mapper)
        {
            _monthlyFinancialSummaryRepository = repository;
        }

    }
}
