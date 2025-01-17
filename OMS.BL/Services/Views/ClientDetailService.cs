using OMS.BL.Dtos.Views;
using OMS.BL.IServices.Views;
using OMS.BL.Mapping;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientDetailService : GenericViewService<ClientDetail, ClientDetailDto>, IClientDetailService
    {
        private readonly IClientDetailRepository _clientDetailRepository;

        public ClientDetailService(IGenericViewRepository<ClientDetail> genericRepo,
                                   IMapperService mapper,
                                   IClientDetailRepository repository) : base(genericRepo, mapper)
        {
            _clientDetailRepository = repository;
        }

        /*
                 public async Task<IEnumerable<ClientDetailDto>> GetAllClientsDetailAsync()
        {
            IEnumerable<ClientDetail> clientDetail = await _repository.GetAllAsync();

            return clientDetail?.Select(c => new ClientDetailDto
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                Phone = c.Phone,
                ClientType = c.ClientType

            }) ?? Enumerable.Empty<ClientDetailDto>();
        }

        public async Task<ClientDetailDto?> GetClientDetailByIdAsync(int clientId)
        {
            ClientDetail? client = await _repository.GetByIdAsync(clientId);

            return client == null ? null : new ClientDetailDto
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                Phone = client.Phone,
                ClientType = client.ClientType
            };
        }
         */

    }
}
