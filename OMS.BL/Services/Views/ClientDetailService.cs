using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientDetailService : GenericViewService<ClientDetail, ClientDetailModel>, IClientDetailService
    {
        private readonly IClientDetailRepository _clientDetailRepository;

        public ClientDetailService(IGenericViewRepository<ClientDetail> genericRepo,
                                   IMapperService mapper,
                                   IClientDetailRepository repository) : base(genericRepo, mapper)
        {
            _clientDetailRepository = repository;
        }

    }
}
