using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.UOW;

namespace OMS.BL.Services.Tables
{
    public class ClientService : GenericService<Client, ClientModel>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountService _accountService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IGenericRepository<Client> genericRepo,
                             IMapperService mapper,
                             IClientRepository repository,
                             IAccountService accountService,
                             IUnitOfWork unitOfWork) : base(genericRepo, mapper)
        {
            _clientRepository = repository;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
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

        public async Task<bool> AddWithAccountAsync(ClientAccountModel model)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            var isClientSaved = await AddAsync(model.Client);
            if (!isClientSaved) return false;

            model.Account.ClientId = model.Client.Id;

            var isAccountSaved = await _accountService.AddAsync(model.Account);
            if (!isAccountSaved) return false;

            await transaction.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateWithAccountAsync(ClientAccountModel model)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            var isClientSaved = await UpdateAsync(model.Client);
            if (!isClientSaved) return false;

            if (model.Account.Id == 0)
            {
                model.Account.ClientId = model.Client.Id;
                var isAccountAdded = await _accountService.AddAsync(model.Account);
                if (!isAccountAdded) return false;
            }
            else
            {
                var isAccountUpdated = await _accountService.UpdateAsync(model.Account);
                if (!isAccountUpdated) return false;
            }

            await transaction.CommitAsync();
            return true;
        }
    }
}
