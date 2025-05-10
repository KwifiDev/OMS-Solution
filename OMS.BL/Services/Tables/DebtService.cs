using OMS.BL.Models.StoredProcedureParams;
using OMS.BL.Models.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
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

      
    }
}
