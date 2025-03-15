using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);
    }
}
