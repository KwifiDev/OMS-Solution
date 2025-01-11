using OMS.BL.Models.DTOs_StoredProcedures;
using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IClientService
    {
        Task<IEnumerable<ClientModel>> GetAllClientsAsync();
        Task<ClientModel?> GetClientByIdAsync(int clientId);
        Task<bool> AddClientAsync(ClientModel model);
        Task<bool> UpdateClientAsync(ClientModel model);
        Task<bool> DeleteClientAsync(int clientId);
        Task<bool> PayAllDebtsById(PayDebtsModel model);
    }
}
