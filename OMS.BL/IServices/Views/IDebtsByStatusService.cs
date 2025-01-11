using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IDebtsByStatusService
    {
        Task<IEnumerable<DebtsByStatusModel>> GetAllDebtsByStatusAsync();

    }
}
