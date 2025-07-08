using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class PersonDetailService : GenericViewService<PersonDetail, PersonDetailModel>, IPersonDetailService
    {
        private readonly IPersonDetailRepository _personDetailRepository;

        public PersonDetailService(IGenericViewRepository<PersonDetail> genericRepo,
                                   IMapperService mapper,
                                   IPersonDetailRepository repository) : base(genericRepo, mapper)
        {
            _personDetailRepository = repository;
        }
    }
}
