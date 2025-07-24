using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMS.BL.IServices.Tables;
using OMS.BL.Mapping;
using OMS.BL.Models.Hybrid;
using OMS.BL.Models.Tables;
using OMS.DA.Entities;

namespace OMS.BL.Services.Tables
{
    public class AuthService : IAuthService
    {
        private readonly IPersonService _personService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapperService _mapperService;

        public AuthService(IPersonService personService, UserManager<User> userManager, SignInManager<User> signInManager, IMapperService mapperService)
        {
            _personService = personService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapperService = mapperService;
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

            int userId = Convert.ToInt32(await _userManager.GetUserIdAsync(user));
            model.UserId = userId;


            return result.Succeeded;
        }

        public async Task<UserLoginModel?> SignInAsync(RequestLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded) return null;

            User? user = await _userManager.Users
                               .Include(u => u.Person)
                               .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user == null || user.Person == null) return null;

            return _mapperService.Map<User, UserLoginModel>(user!);
        }

        public async Task<bool> RegisterWithPersonAsync(FullRegisterModel model)
        {
            var personModel = _mapperService.Map<FullRegisterModel, PersonModel>(model);

            var isPersonAdded = await _personService.AddAsync(personModel);

            if (!isPersonAdded) return false;

            model.PersonId = personModel.PersonId;

            var registerModel = _mapperService.Map<FullRegisterModel, RegisterModel>(model);

            var isSuccess = await RegisterAsync(registerModel);

            return isSuccess;
        }
    }
}
