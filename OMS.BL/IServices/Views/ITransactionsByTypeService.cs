using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface ITransactionsByTypeService
    {
        Task<IEnumerable<TransactionsByTypeModel>> GetAllTransactionsByTypeAsync();
    }
}
