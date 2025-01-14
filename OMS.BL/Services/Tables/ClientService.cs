using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.Enums;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class ClientService : GenericService<Client, ClientDto>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IGenericRepository<Client> repo,
                             IMapperService mapper,
                             IClientRepository repository) : base(repo, mapper)
        {
            _clientRepository = repository;
        }

        public async Task<bool> PayAllDebtsById(PayDebtsDto dto)
        {
            dto.PayDebtStatus = await _clientRepository.PayAllDebtsByIdAsync
                (
                    dto.ClientId,
                    dto.Notes,
                    dto.CreatedByUserId
                );

            return dto.PayDebtStatus == EnPayDebtStatus.Success;
        }

        /*
         public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            IEnumerable<Client> clients = await _repository.GetAllAsync();

            return clients?.Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                PersonId = c.PersonId,
                ClientType = c.ClientType

            }) ?? Enumerable.Empty<ClientDto>();
        }

        public async Task<ClientDto?> GetClientByIdAsync(int clientId)
        {
            Client? client = await _repository.GetByIdAsync(clientId);

            return client == null ? null : new ClientDto
            {
                ClientId = client.ClientId,
                PersonId = client.PersonId,
                ClientType = client.ClientType
            };
        }

        public async Task<bool> AddClientAsync(ClientDto dto)
        {
            if (dto == null) return false;

            Client client = new Client
            {
                ClientId = dto.ClientId,
                PersonId = dto.PersonId,
                ClientType = dto.ClientType
            };

            bool success = await _repository.AddAsync(client);

            if (success) dto.ClientId = client.ClientId;

            return success;
        }

        public async Task<bool> UpdateClientAsync(ClientDto dto)
        {
            if (dto == null) return false;

            Client? client = await _repository.GetByIdAsync(dto.ClientId);

            if (client == null) return false;

            client.ClientType = dto.ClientType;

            return await _repository.UpdateAsync(client);

        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            if (clientId <= 0) return false;

            return await _repository.DeleteAsync(clientId);
        }
         */
    }
}
