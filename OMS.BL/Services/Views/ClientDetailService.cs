using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientDetailService : IClientDetailService
    {
        private readonly IClientDetailRepository _repository;

        public ClientDetailService(IClientDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientDetailModel>> GetAllClientsDetailAsync()
        {
            IEnumerable<ClientDetail> clientDetail = await _repository.GetAllAsync();

            return clientDetail?.Select(c => new ClientDetailModel
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                Phone = c.Phone,
                ClientType = c.ClientType

            }) ?? Enumerable.Empty<ClientDetailModel>();
        }

        public async Task<ClientDetailModel?> GetClientDetailByIdAsync(int clientId)
        {
            ClientDetail? client = await _repository.GetClientDetailByIdAsync(clientId);

            return client == null ? null : new ClientDetailModel
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                Phone = client.Phone,
                ClientType = client.ClientType
            };
        }

    }
}
