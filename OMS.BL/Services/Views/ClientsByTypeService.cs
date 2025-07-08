using OMS.BL.Models.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsByTypeService : GenericViewService<ClientsByType, ClientsByTypeModel>, IClientsByTypeService
    {
        private readonly IClientsByTypeRepository _clientsByTypeRepository;

        public ClientsByTypeService(IGenericViewRepository<ClientsByType> genericRepo,
                                    IMapperService mapper,
                                    IClientsByTypeRepository repository) : base(genericRepo, mapper)
        {
            _clientsByTypeRepository = repository;
        }

    }
}
