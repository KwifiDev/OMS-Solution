using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceModel>> GetAllServicesAsync();
        Task<ServiceModel?> GetServiceByIdAsync(int serviceId);
        Task<bool> AddServiceAsync(ServiceModel model);
        Task<bool> UpdateServiceAsync(ServiceModel model);
        Task<bool> DeleteServiceAsync(int serviceId);
    }
}
