using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class UserService : GenericService<User, UserModel>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IGenericRepository<User> genericRepo,
                           IMapperService mapper,
                           IUserRepository repository) : base(genericRepo, mapper)
        {
            _userRepository = repository;
        }

        public override async Task<UserModel?> GetByIdAsync(int id)
        {
            if (id <= 0) return default(UserModel);

            User? user = await _userRepository.GetByIdAsync(id);

            return user == null ? default(UserModel) : _mapperService.Map<User, UserModel>(user);
        }

        public override async Task<bool> UpdateAsync(UserModel userModel)
        {
            var user = _mapperService.Map<UserModel, User>(userModel);

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<UserLoginModel?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            User? user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            return user == null ? null : _mapperService.Map<User, UserLoginModel>(user);
        }

        public async Task<UserLoginModel?> GetUserLoginByPersonIdAsync(int personId)
        {
            User? user = await _userRepository.GetUserLoginByPersonIdAsync(personId);

            return user == null ? null : _mapperService.Map<User, UserLoginModel>(user);
        }

        public async Task<UserModel?> GetByPersonIdAsync(int personId)
        {
            User? user = await _userRepository.GetByPersonIdAsync(personId);

            return user == null ? null : _mapperService.Map<User, UserModel>(user);
        }

        public async Task<int> GetIdByPersonIdAsync(int personId)
        {
            return await _userRepository.GetIdByPersonIdAsync(personId);
        }

        public async Task<bool> IsUserActive(int userId)
            => await _userRepository.IsUserActive(userId);

        public async Task<bool> UpdateUserActivationStatus(int userId, bool isActive)
            => await _userRepository.UpdateUserActivationStatus(userId, isActive);

        public async Task<bool> IsUsernameUsedAsync(int userId,string username)
            => await _userRepository.IsUsernameUsedAsync(userId, username);


        public async Task<string?> GetUsernameById(int userId)
            => await _userRepository.GetUsernamebyId(userId);

    }
}
