using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);

        Task<User?> GetUserLoginByPersonIdAsync(int personId);

        Task<User?> GetByPersonIdAsync(int person);

        Task<int> GetIdByPersonIdAsync(int personId);
    }
}
