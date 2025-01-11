using OMS.BL.Models.Views;

namespace OMS.BL.IServices.Views
{
    public interface IClientsByTypeService
    {
        Task<IEnumerable<ClientsByTypeModel>> GetAllClientsByTypeAsync();
    }
}
