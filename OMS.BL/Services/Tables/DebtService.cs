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

        public async Task<bool> PayDebtByIdAsync(PayDebtModel dto)
        {
            dto.PayDebtStatus = await _debtRepository.PayDebtByIdAsync
                (
                    debtId: dto.DebtId,
                    notes: dto.Notes,
                    createdByUserId: dto.CreatedByUserId
                );

            return dto.PayDebtStatus == EnPayDebtStatus.Success;
        }

      
    }
}
