using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities;
using OMS.DA.IRepositories.IEntityRepos;

namespace OMS.BL.Services.Tables
{
    public class UserService : GenericService<User, UserModel>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IGenericRepository<User> genericRepo,
                           IMapperService mapper,
                           IUserRepository repository,
                           UserManager<User> userManager) : base(genericRepo, mapper)
        {
            _userRepository = repository;
            _userManager = userManager;
        }

        public override async Task<UserModel?> GetByIdAsync(int id)
        {
            if (id <= 0) return default;

            User? user = await _userRepository.GetByIdAsync(id);

            return user == null ? default : _mapperService.Map<User, UserModel>(user);
        }

        public override Task<bool> AddAsync(UserModel model)
            => Task.FromResult(false);

        public override Task<bool> UpdateAsync(UserModel userModel)
            => Task.FromResult(false);

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

        public async Task<bool?> IsUsernameValid(UserModel model)
        {
            string? oldUsername = await GetUsernameById(model.UserId);

            if (oldUsername == null) return null;

            if (oldUsername != model.UserName)
            {
                if (await _userRepository.IsUsernameUsedAsync(model.UserId, model.UserName)) return false;
            }

            return true;
        }

        public async Task<EnUserResult> UpdateUserAsync(UserModel model)
        {
            var isValid = await IsUsernameValid(model);

            if (isValid is null) return EnUserResult.NotFound;

            if (isValid == false) return EnUserResult.UserNameConflict;

            var user = _mapperService.Map<UserModel, User>(model);

            var isSuccess = await _userRepository.UpdateAsync(user);

            if (!isSuccess) return EnUserResult.ChangeBranchIdFaild;

            var userIdentity = await _userManager.FindByIdAsync(user.Id.ToString());

            var result = await _userManager.SetUserNameAsync(userIdentity!, user.UserName);

            if (!result.Succeeded) return EnUserResult.ChangeUserNameFaild;

            return EnUserResult.Success;
        }

    }
}
