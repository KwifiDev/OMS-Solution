using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.BL.Services.Security;
using OMS.Common.Enums;
using OMS.DA.Entities.Identity;
using OMS.DA.UOW;
using System.IdentityModel.Tokens.Jwt;

namespace OMS.BL.Services.Tables
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapperService _mapperService;

        private readonly IPersonService _personService;
        private readonly IRoleService _roleService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapperService mapperService,
                           IPersonService personService, IRoleService roleService, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapperService = mapperService;
            _personService = personService;
            _roleService = roleService;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            if (user is null) return false;

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return result.Succeeded;
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var user = _mapperService.Map<RegisterModel, User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return false;

            model.UserId = Convert.ToInt32(await _userManager.GetUserIdAsync(user));
            return true;
        }

        public async Task<(TokenModel TokenInfo, UserLoginModel UserLogin)> LoginAsync(LoginModel model)
        {
            var user = await _userManager.Users
                             .Include(u => u.Person)
                             .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user is null || user.Person is null) return default;

            var isSigningIn = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isSigningIn) return default;

            var userLoginModel = _mapperService.Map<User, UserLoginModel>(user!);

            var jwtSecurityToken = await _tokenService.GenerateToken(user);
            if (jwtSecurityToken is null) return default;

            var tokenInfo = new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expires = jwtSecurityToken.ValidTo,
                TokenType = "Bearer"
            };

            return (tokenInfo, userLoginModel);
        }

        public async Task<bool> RegisterUserWithProfileAsync(FullRegisterModel model)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var personModel = _mapperService.Map<FullRegisterModel, PersonModel>(model);

                var isPersonAdded = await _personService.AddAsync(personModel);
                if (!isPersonAdded) return false;

                model.PersonId = personModel.PersonId;

                var registerModel = _mapperService.Map<FullRegisterModel, RegisterModel>(model);

                var isSuccess = await RegisterAsync(registerModel);
                if (!isSuccess) return false;

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return Enumerable.Empty<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<EnAuthResult> AddUserToRoleAsync(UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user is null) return EnAuthResult.UserNotFound;

            var role = await _roleService.FindByNameAsync(model.RoleName);
            if (role is null) return EnAuthResult.RoleNotFound;

            var isInRole = await _userManager.IsInRoleAsync(user, model.RoleName);
            if (isInRole) return EnAuthResult.Conflict;

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            return result.Succeeded ? EnAuthResult.Success : EnAuthResult.Failed;
        }

        public async Task<EnAuthResult> RemoveUserFromRoleAsync(UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user is null) return EnAuthResult.UserNotFound;

            var role = await _roleService.FindByNameAsync(model.RoleName);
            if (role is null) return EnAuthResult.RoleNotFound;

            var isInRole = await _userManager.IsInRoleAsync(user, model.RoleName);
            if (!isInRole) return EnAuthResult.RoleNotFound;

            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            return result.Succeeded ? EnAuthResult.Success : EnAuthResult.Failed;
        }

        public async Task<bool> ChangeUserRolesAsync(InputUserRolesModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user is null) return false;

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (model.RolesToAdd.Count > 0)
                {
                    var addResult = await _userManager.AddToRolesAsync(user, model.RolesToAdd);
                    if (!addResult.Succeeded) return false;
                }

                if (model.RolesToRemove.Count > 0)
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, model.RolesToRemove);
                    if (!removeResult.Succeeded) return false;
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool?> IsUserInRoleAsync(UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user is null) return null;

            return await _userManager.IsInRoleAsync(user, model.RoleName);
        }

    }
}
