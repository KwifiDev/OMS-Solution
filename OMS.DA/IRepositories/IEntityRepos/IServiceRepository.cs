using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllServicesOption();
    }
}
