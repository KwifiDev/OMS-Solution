using OMS.DA.Entities;

namespace OMS.DA.IRepositories.IEntityRepos
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(User user);

        Task<User?> GetByUsernameAndPasswordAsync(string username, string password);

        Task<User?> GetUserLoginByPersonIdAsync(int personId);

        Task<User?> GetByPersonIdAsync(int person);

        Task<int> GetIdByPersonIdAsync(int personId);

        Task<bool> IsUserActive(int userId);

        Task<bool> UpdateUserActivationStatus(int userId, bool isActive);

        Task<bool> IsUsernameUsedAsync(int userId, string username);

        Task<string?> GetUsernamebyId(int userId);

        Task<bool> UpdatePassword(int userId, string newPassword);

        Task<string?> GetPasswordById(int userId);
    }
}
