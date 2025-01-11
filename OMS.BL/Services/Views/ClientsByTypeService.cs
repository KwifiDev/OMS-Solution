using OMS.BL.IServices.Views;
using OMS.BL.Models.Views;
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

        public async Task<IEnumerable<ClientsByTypeModel>> GetAllClientsByTypeAsync()
        {
            IEnumerable<ClientsByType> accountBalancesTransaction = await _repository.GetAllAsync();

            return accountBalancesTransaction?.Select(c => new ClientsByTypeModel
            {
                ClientType = c.ClientType,
                TotalClients = c.TotalClients

            }) ?? Enumerable.Empty<ClientsByTypeModel>();
        }

    }
}
