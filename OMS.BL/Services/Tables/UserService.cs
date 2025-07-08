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

        public async Task<UserLoginModel?> GetByUsernameAndPasswordAsync(RequestLoginModel model)
        {
            User? user = await _userRepository.GetByUsernameAndPasswordAsync(model.Username, model.Password);

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

        public async Task<bool> IsUsernameUsedAsync(int userId, string username)
            => await _userRepository.IsUsernameUsedAsync(userId, username);


        public async Task<string?> GetUsernameById(int userId)
            => await _userRepository.GetUsernamebyId(userId);

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var oldPassword = await _userRepository.GetPasswordById(changePasswordModel.UserId);

            if (oldPassword == null || oldPassword.Trim() != changePasswordModel.OldPassword.Trim()) return false;

            bool isChanged = await _userRepository.UpdatePassword(changePasswordModel.UserId, changePasswordModel.NewPassword);

            return isChanged;
        }

        public async Task<bool?> IsUsernameValid(UserModel model)
        {
            string? oldUsername = await GetUsernameById(model.UserId);

            if (oldUsername == null) return null;

            if (oldUsername != model.Username)
            {
                if (await _userRepository.IsUsernameUsedAsync(model.UserId, model.Username)) return false;
            }

            return true;
        }

    }
}
