using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class DebtService : GenericService<Debt, DebtModel>, IDebtService
    {
        private readonly IDebtRepository _debtRepository;

        public DebtService(IGenericRepository<Debt> genericRepo,
                           IMapperService mapper,
                           IDebtRepository repository) : base(genericRepo, mapper)
        {
            _debtRepository = repository;
        }

        public async Task<bool> PayDebtByIdAsync(PayDebtModel model)
        {
            model.PayDebtStatus = await _debtRepository.PayDebtByIdAsync
                (
                    debtId: model.DebtId,
                    notes: model.Notes,
                    createdByUserId: model.CreatedByUserId
                );

            return model.PayDebtStatus == EnPayDebtStatus.Success;
        }

        public async Task<bool> AddDebtAsync(DebtCreationModel model)
        {
            model.DebtId = await _debtRepository.AddDebtAsync
                (
                    clientId: model.ClientId,
                    serviceId: model.ServiceId,
                    quantity: model.Quantity,
                    description: model.Description,
                    notes: model.Notes,
                    createdByUserId: model.CreatedByUserId
                );

            return model.DebtId > 0;
        }

        public async Task<bool?> CancelDebtAsync(int debtId)
        {
            var debtModel = await GetByIdAsync(debtId);

            if (debtModel == null) return null;

            if (debtModel.Status == EnDebtStatus.Paid || debtModel.Status == EnDebtStatus.Canceled) return false;

            debtModel.Status = EnDebtStatus.Canceled;

            return await UpdateAsync(debtModel);
        }

        public async Task<decimal?> CalcTotalDebtsByClientIdAsync(int clientId)
        {
            return await _debtRepository.CalcTotalDebtsByClientIdAsync(clientId);
        }
    }
}
