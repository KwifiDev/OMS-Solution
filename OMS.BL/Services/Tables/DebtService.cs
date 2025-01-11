using OMS.BL.IServices.Tables;
using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.Enums;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class DebtService : IDebtService
    {
        private readonly IDebtRepository _repository;

        public DebtService(IDebtRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DebtModel>> GetAllDebtsAsync()
        {
            IEnumerable<Debt> debts = await _repository.GetAllAsync();

            return debts?.Select(d => new DebtModel
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

            }) ?? Enumerable.Empty<DebtModel>();
        }

        public async Task<DebtModel?> GetDebtByIdAsync(int debtId)
        {
            Debt? debt = await _repository.GetByIdAsync(debtId);

            return debt == null ? null : new DebtModel
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

        public async Task<bool> AddDebtAsync(DebtModel model)
        {
            if (model == null) return false;

            Debt debt = new Debt
            {
                ClientId = model.ClientId,
                ServiceId = model.ServiceId,
                Quantity = model.Quantity,
                Description = model.Description,
                Notes = model.Notes,
                CreatedByUserId = model.CreatedByUserId,
            };

            bool success = await _repository.AddAsync(debt);

            if (success) model.DebtId = debt.DebtId;

            return success;
        }

        public async Task<bool> UpdateDebtAsync(DebtModel model)
        {
            if (model == null) return false;

            Debt? debt = await _repository.GetByIdAsync(model.DebtId);

            if (debt == null) return false;

            debt.Description = model.Description;
            debt.Notes = model.Notes;

            return await _repository.UpdateAsync(debt);
        }

        public async Task<bool> DeleteDebtAsync(int debtId)
        {
            if (debtId <= 0) return false;

            return await _repository.DeleteAsync(debtId);
        }

        public async Task<bool> PayDebtByIdAsync(PayDebtModel model)
        {
            model.PayDebtStatus = await _repository.PayDebtByIdAsync
                (
                    debtId: model.DebtId,
                    notes: model.Notes,
                    createdByUserId: model.CreatedByUserId
                );

            return model.PayDebtStatus == EnPayDebtStatus.Success;
        }
    }
}
