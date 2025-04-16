using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class ClientService : GenericService<Client, ClientDto>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IGenericRepository<Client> genericRepo,
                             IMapperService mapper,
                             IClientRepository repository) : base(genericRepo, mapper)
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


        public async Task<ClientDto?> GetByPersonIdAsync(int personId)
        {
            var client = await _clientRepository.GetByPersonIdAsync(personId);

            return client != null ? _mapperService.Map<Client, ClientDto>(client) : null;
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _clientRepository.GetIdByPersonIdAsync(personId);
        }
    }
}
