using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class DashboardSummaryService : GenericViewService<DashboardSummary, DashboardSummaryModel>, IDashboardSummaryService
    {
        public DashboardSummaryService(IGenericViewRepository<DashboardSummary> genericRepo,
                                       IMapperService mapper) : base(genericRepo, mapper)
        {
        }
    }
}
