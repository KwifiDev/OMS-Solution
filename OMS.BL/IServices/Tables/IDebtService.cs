using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;
using OMS.DA.Enums;

namespace OMS.BL.IServices.Tables
{
    public interface IDebtService
    {
        Task<IEnumerable<DebtModel>> GetAllDebtsAsync();
        Task<DebtModel?> GetDebtByIdAsync(int debtId);
        Task<bool> AddDebtAsync(DebtModel model);
        Task<bool> UpdateDebtAsync(DebtModel model);
        Task<bool> DeleteDebtAsync(int debtId);
        Task<bool> PayDebtByIdAsync(PayDebtModel model);
    }
}
