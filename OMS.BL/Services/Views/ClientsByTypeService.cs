using OMS.BL.IServices.Views;
using OMS.BL.Dtos.Views;
using OMS.DA.IRepositories.IViewRepos;
using OMS.DA.Views;

namespace OMS.BL.Services.Views
{
    public class ClientsByTypeService : IClientsByTypeService
    {
        private readonly IClientsByTypeRepository _repository;

        public ClientsByTypeService(IClientsByTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientsByTypeDto>> GetAllClientsByTypeAsync()
        {
            IEnumerable<ClientsByType> accountBalancesTransaction = await _repository.GetAllAsync();

            return accountBalancesTransaction?.Select(c => new ClientsByTypeDto
            {
                ClientType = c.ClientType,
                TotalClients = c.TotalClients

            }) ?? Enumerable.Empty<ClientsByTypeDto>();
        }

    }
}
