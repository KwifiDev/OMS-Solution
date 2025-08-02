using Microsoft.AspNetCore.Identity;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities.Identity;
using OMS.DA.IRepositories.IEntityRepos;
using OMS.DA.UOW;

namespace OMS.BL.Services.Tables
{
    public class UserService : GenericService<User, UserModel>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IGenericRepository<User> genericRepo,
                           IMapperService mapper,
                           IUserRepository repository,
                           UserManager<User> userManager,
                           IUnitOfWork unitOfWork) : base(genericRepo, mapper)
        {
            _userRepository = repository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public override Task<bool> AddAsync(UserModel model)
           => throw new NotImplementedException("This future not avalible");

        public override Task<bool> UpdateAsync(UserModel userModel)
            => throw new NotImplementedException("This future not avalible");



        public override async Task<UserModel?> GetByIdAsync(int id)
        {
            if (id <= 0) return default;

            User? user = await _userRepository.GetByIdAsync(id);

            return user == null ? default : _mapperService.Map<User, UserModel>(user);
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
            => await _userRepository.GetIdByPersonIdAsync(personId);

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

            if (oldUsername == model.UserName) return true;

            return !(await _userRepository.IsUsernameUsedAsync(model.UserId, model.UserName));
        }

        private async Task<bool> UpdateUserBranchAsync(User user)
           => await _userRepository.UpdateAsync(user);

        private async Task<EnUserResult> UpdateUserNameAsync(User user)
        {
            var userIdentity = await _userManager.FindByIdAsync(user.Id.ToString());
            if (userIdentity == null) return EnUserResult.NotFound;

            var result = await _userManager.SetUserNameAsync(userIdentity, user.UserName);
            return result.Succeeded ? EnUserResult.Success : EnUserResult.ChangeUserNameFaild;
        }

        public async Task<EnUserResult> UpdateUserAsync(UserModel model)
        {
            var isValid = await IsUsernameValid(model);
            if (isValid is null) return EnUserResult.NotFound;
            if (isValid == false) return EnUserResult.UserNameConflict;

            var user = _mapperService.Map<UserModel, User>(model);

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var isBranchUpdated = await UpdateUserBranchAsync(user);
                if (!isBranchUpdated) return EnUserResult.ChangeBranchIdFaild;

                var result = await UpdateUserNameAsync(user);

                if (result != EnUserResult.Success)
                {
                    await transaction.RollbackAsync();
                    return EnUserResult.UserNameConflict;
                }

                await transaction.CommitAsync();
                return EnUserResult.Success;
            }
            catch
            {
                await transaction.RollbackAsync();
                return EnUserResult.NotFound;
            }
        }

    }
}
