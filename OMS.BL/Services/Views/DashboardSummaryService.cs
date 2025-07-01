using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DashboardSummaryService : IDashboardSummaryService
    {
        private readonly IDashboardSummaryRepository _dashboardSummaryRepository;
        private readonly IMapperService _mapper;

        public DashboardSummaryService(IDashboardSummaryRepository dashboardSummaryRepository, IMapperService mapper)
        {
            _dashboardSummaryRepository = dashboardSummaryRepository;
            _mapper = mapper;
        }

        public async Task<DashboardSummaryModel?> GetData()
        {
            var dashboardSummary = await _dashboardSummaryRepository.GetData();
            return dashboardSummary != null ? _mapper.Map<DashboardSummary, DashboardSummaryModel>(dashboardSummary) : default;
        }
    }
}
