using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            IEnumerable<User> Users = await _repository.GetAllAsync();

            return Users?.Select(u => new UserModel
            {
                UserId = u.UserId,
                PersonId = u.PersonId,
                BranchId = u.BranchId,
                Username = u.Username,
                Password = u.Password,
                Permissions = u.Permissions,
                IsActive = u.IsActive

            }) ?? Enumerable.Empty<UserModel>();
        }

        public async Task<UserModel?> GetUserByIdAsync(int userId)
        {
            User? user = await _repository.GetByIdAsync(userId);

            return user == null ? null : new UserModel
            {
                UserId = user.UserId,
                PersonId = user.PersonId,
                BranchId = user.BranchId,
                Username = user.Username,
                Password = user.Password,
                Permissions = user.Permissions,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> AddUserAsync(UserModel model)
        {
            if (model == null) return false;

            User user = new User
            {
                PersonId = model.PersonId,
                BranchId = model.BranchId,
                Username = model.Username,
                Password = model.Password,
                Permissions = model.Permissions,
                IsActive = model.IsActive
            };

            bool success = await _repository.AddAsync(user);

            if (success) model.UserId = user.UserId;

            return success;
        }

        public async Task<bool> UpdateUserAsync(UserModel model)
        {
            if (model == null) return false;

            User? user = await _repository.GetByIdAsync(model.UserId);

            if (user == null) return false;

            user.BranchId = model.BranchId;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Permissions = model.Permissions;
            user.IsActive = model.IsActive;

            return await _repository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0) return false;

            return await _repository.DeleteAsync(userId);
        }
    }
}
