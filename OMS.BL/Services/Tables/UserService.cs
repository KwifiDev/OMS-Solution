using OMS.BL.IServices.Tables;
using OMS.BL.Dtos.Tables;
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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            IEnumerable<User> Users = await _repository.GetAllAsync();

            return Users?.Select(u => new UserDto
            {
                UserId = u.UserId,
                PersonId = u.PersonId,
                BranchId = u.BranchId,
                Username = u.Username,
                Password = u.Password,
                Permissions = u.Permissions,
                IsActive = u.IsActive

            }) ?? Enumerable.Empty<UserDto>();
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            User? user = await _repository.GetByIdAsync(userId);

            return user == null ? null : new UserDto
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

        public async Task<bool> AddUserAsync(UserDto dto)
        {
            if (dto == null) return false;

            User user = new User
            {
                PersonId = dto.PersonId,
                BranchId = dto.BranchId,
                Username = dto.Username,
                Password = dto.Password,
                Permissions = dto.Permissions,
                IsActive = dto.IsActive
            };

            bool success = await _repository.AddAsync(user);

            if (success) dto.UserId = user.UserId;

            return success;
        }

        public async Task<bool> UpdateUserAsync(UserDto dto)
        {
            if (dto == null) return false;

            User? user = await _repository.GetByIdAsync(dto.UserId);

            if (user == null) return false;

            user.BranchId = dto.BranchId;
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.Permissions = dto.Permissions;
            user.IsActive = dto.IsActive;

            return await _repository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0) return false;

            return await _repository.DeleteAsync(userId);
        }
    }
}
