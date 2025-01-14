using OMS.BL.Dtos.StoredProcedureParams;
using OMS.BL.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.DA.Entities;
using OMS.DA.Enums;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class DebtService : GenericService<Debt, DebtDto>, IDebtService
    {
        private readonly IDebtRepository _debtRepository;

        public DebtService(IGenericRepository<Debt> repo,
                           IMapperService mapper,
                           IDebtRepository repository) : base(repo, mapper)
        {
            _debtRepository = repository;
        }

        public async Task<bool> PayDebtByIdAsync(PayDebtDto dto)
        {
            dto.PayDebtStatus = await _debtRepository.PayDebtByIdAsync
                (
                    debtId: dto.DebtId,
                    notes: dto.Notes,
                    createdByUserId: dto.CreatedByUserId
                );

            return dto.PayDebtStatus == EnPayDebtStatus.Success;
        }

        /*
                 public async Task<IEnumerable<DebtDto>> GetAllDebtsAsync()
        {
            IEnumerable<Debt> debts = await _repository.GetAllAsync();

            return debts?.Select(d => new DebtDto
            {
                DebtId = d.DebtId,
                ClientId = d.ClientId,
                ServiceId = d.ServiceId,
                Cost = d.Cost,
                Quantity = d.Quantity,
                DiscountPercentage = d.DiscountPercentage,
                AmountDeducted = d.AmountDeducted,
                Total = d.Total,
                Description = d.Description,
                Notes = d.Notes,
                CreatedAt = d.CreatedAt,
                Status = d.Status,
                PaymentId = d.PaymentId,
                CreatedByUserId = d.CreatedByUserId,

            }) ?? Enumerable.Empty<DebtDto>();
        }

        public async Task<DebtDto?> GetDebtByIdAsync(int debtId)
        {
            Debt? debt = await _repository.GetByIdAsync(debtId);

            return debt == null ? null : new DebtDto
            {
                DebtId = debt.DebtId,
                ClientId = debt.ClientId,
                ServiceId = debt.ServiceId,
                Cost = debt.Cost,
                Quantity = debt.Quantity,
                DiscountPercentage = debt.DiscountPercentage,
                AmountDeducted = debt.AmountDeducted,
                Total = debt.Total,
                Description = debt.Description,
                Notes = debt.Notes,
                CreatedAt = debt.CreatedAt,
                Status = debt.Status,
                PaymentId = debt.PaymentId,
                CreatedByUserId = debt.CreatedByUserId,
            };
        }

        public async Task<bool> AddDebtAsync(DebtDto dto)
        {
            if (dto == null) return false;

            Debt debt = new Debt
            {
                ClientId = dto.ClientId,
                ServiceId = dto.ServiceId,
                Quantity = dto.Quantity,
                Description = dto.Description,
                Notes = dto.Notes,
                CreatedByUserId = dto.CreatedByUserId,
            };

            bool success = await _repository.AddAsync(debt);

            if (success) dto.DebtId = debt.DebtId;

            return success;
        }

        public async Task<bool> UpdateDebtAsync(DebtDto dto)
        {
            if (dto == null) return false;

            Debt? debt = await _repository.GetByIdAsync(dto.DebtId);

            if (debt == null) return false;

            debt.Description = dto.Description;
            debt.Notes = dto.Notes;

            return await _repository.UpdateAsync(debt);
        }

        public async Task<bool> DeleteDebtAsync(int debtId)
        {
            if (debtId <= 0) return false;

            return await _repository.DeleteAsync(debtId);
        }
         */
    }
}
