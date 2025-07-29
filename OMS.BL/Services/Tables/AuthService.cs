using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.Common.Enums;
using OMS.DA.Entities;

namespace OMS.BL.Services.Tables
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapperService _mapperService;

        private readonly IPersonService _personService;
        private readonly IRoleService _roleService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapperService mapperService,
                           IPersonService personService, IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapperService = mapperService;
            _personService = personService;
            _roleService = roleService;
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

        public async Task<UserLoginModel?> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded) return null;

            User? user = await _userManager.Users
                               .Include(u => u.Person)
                               .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user == null || user.Person == null) return null;

            return _mapperService.Map<User, UserLoginModel>(user!);
        }

        public async Task<bool> RegisterUserWithProfileAsync(FullRegisterModel model)
        {
            var personModel = _mapperService.Map<FullRegisterModel, PersonModel>(model);

            var isPersonAdded = await _personService.AddAsync(personModel);

            if (!isPersonAdded) return false;

            model.PersonId = personModel.PersonId;

            var registerModel = _mapperService.Map<FullRegisterModel, RegisterModel>(model);

            var isSuccess = await RegisterAsync(registerModel);

            return isSuccess;
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

        public async Task<bool?> IsUserInRoleAsync(UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user is null) return null;

            return await _userManager.IsInRoleAsync(user, model.RoleName);
        }
    }
}
