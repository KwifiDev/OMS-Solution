using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class RolesSummaryService : GenericViewService<RolesSummary, RolesSummaryModel>, IRolesSummaryService
    {
        public RolesSummaryService(IGenericViewRepository<RolesSummary> repository, IMapperService mapper) : base(repository, mapper)
        {
        }
    }
}
