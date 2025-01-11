using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IClientDetailService
    {
        Task<IEnumerable<ClientDetailModel>> GetAllClientsDetailAsync();
        Task<ClientDetailModel?> GetClientDetailByIdAsync(int clientId);
    }
}
