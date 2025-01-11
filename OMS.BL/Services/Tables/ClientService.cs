using OMS.BL.IServices.Tables;
using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.Enums;
using OMS.DA.IRepositories.IEntityRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BL.Services.Tables
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientModel>> GetAllClientsAsync()
        {
            IEnumerable<Client> clients = await _repository.GetAllAsync();

            return clients?.Select(c => new ClientModel
            {
                ClientId = c.ClientId,
                PersonId = c.PersonId,
                ClientType = c.ClientType

            }) ?? Enumerable.Empty<ClientModel>();
        }

        public async Task<ClientModel?> GetClientByIdAsync(int clientId)
        {
            Client? client = await _repository.GetByIdAsync(clientId);

            return client == null ? null : new ClientModel
            {
                ClientId = client.ClientId,
                PersonId = client.PersonId,
                ClientType = client.ClientType
            };
        }

        public async Task<bool> AddClientAsync(ClientModel model)
        {
            if (model == null) return false;

            Client client = new Client
            {
                ClientId = model.ClientId,
                PersonId = model.PersonId,
                ClientType = model.ClientType
            };

            bool success = await _repository.AddAsync(client);

            if (success) model.ClientId = client.ClientId;

            return success;
        }

        public async Task<bool> UpdateClientAsync(ClientModel model)
        {
            if (model == null) return false;

            Client? client = await _repository.GetByIdAsync(model.ClientId);

            if (client == null) return false;

            client.ClientType = model.ClientType;

            return await _repository.UpdateAsync(client);

        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            if (clientId <= 0) return false;

            return await _repository.DeleteAsync(clientId);
        }

        public async Task<bool> PayAllDebtsById(PayDebtsModel model)
        {
            model.PayDebtStatus = await _repository.PayAllDebtsByIdAsync
                (
                    model.ClientId,
                    model.Notes,
                    model.CreatedByUserId
                );

            return model.PayDebtStatus == EnPayDebtStatus.Success;
        }
    }
}
