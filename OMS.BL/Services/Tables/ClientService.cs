using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class ClientService : GenericService<Client, ClientModel>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IGenericRepository<Client> genericRepo,
                             IMapperService mapper,
                             IClientRepository repository) : base(genericRepo, mapper)
        {
            _clientRepository = repository;
        }

        public async Task<bool> PayAllDebtsById(PayDebtsModel model)
        {
            model.PayDebtStatus = await _clientRepository.PayAllDebtsByIdAsync
                (
                    model.ClientId,
                    model.Notes,
                    model.CreatedByUserId
                );

            return model.PayDebtStatus == EnPayDebtStatus.Success;
        }


        public async Task<ClientModel?> GetByPersonIdAsync(int personId)
        {
            var client = await _clientRepository.GetByPersonIdAsync(personId);

            return client != null ? _mapperService.Map<Client, ClientModel>(client) : null;
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _clientRepository.GetIdByPersonIdAsync(personId);
        }
    }
}
