using OMS.BL.Models.Tables;

namespace OMS.BL.IServices.Tables
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonModel>> GetAllPeopleAsync();
        Task<PersonModel?> GetPersonByIdAsync(int personId);
        Task<bool> AddPersonAsync(PersonModel model);
        Task<bool> UpdatePersonAsync(PersonModel model);
        Task<bool> DeletePersonAsync(int personId);
    }
}
