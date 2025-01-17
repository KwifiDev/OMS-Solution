using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsByTypeService : GenericViewService<ClientsByType, ClientsByTypeDto>, IClientsByTypeService
    {
        private readonly IClientsByTypeRepository _clientsByTypeRepository;

        public ClientsByTypeService(IGenericViewRepository<ClientsByType> genericRepo,
                                    IMapperService mapper,
                                    IClientsByTypeRepository repository) : base(genericRepo, mapper)
        {
            _clientsByTypeRepository = repository;
        }



        /*
                 public async Task<IEnumerable<ClientsByTypeDto>> GetAllClientsByTypeAsync()
        {
            IEnumerable<ClientsByType> accountBalancesTransaction = await _repository.GetAllAsync();

            return accountBalancesTransaction?.Select(c => new ClientsByTypeDto
            {
                ClientType = c.ClientType,
                TotalClients = c.TotalClients

            }) ?? Enumerable.Empty<ClientsByTypeDto>();
        }
         */

    }
}
