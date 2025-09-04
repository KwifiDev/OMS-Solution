using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class MonthlyFinancialSummaryService : IMonthlyFinancialSummaryService
    {
        private readonly IMonthlyFinancialSummaryRepository _monthlyFinancialSummaryRepository;
        private readonly IMapperService _mapper;

        public MonthlyFinancialSummaryService(IMonthlyFinancialSummaryRepository repository, IMapperService mapper)
        {
            _monthlyFinancialSummaryRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MonthlyFinancialSummaryModel>> GetAllAsync()
        {
            var data = await _monthlyFinancialSummaryRepository.GetAllAsync();
            return data != null ? _mapper.Map<MonthlyFinancialSummary, MonthlyFinancialSummaryModel>(data) : Enumerable.Empty<MonthlyFinancialSummaryModel>();
        }
    }
}
