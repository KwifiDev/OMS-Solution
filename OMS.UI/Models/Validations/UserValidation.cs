using OMS.UI.APIs.Services.Interfaces.Tables;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OMS.UI.Models.Validations
{
    public class UserValidation
    {
        private readonly IUserService _userService;

        public UserValidation(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ValidationResult?> ValidateFullUsernameAsync(int userId, string username)
        {
            var formatResult = ValidateUsername(username, new ValidationContext(username));
            if (formatResult != ValidationResult.Success)
                return formatResult;

            bool isAvailable = await _userService.CheckUsernameAvailable(userId, username);
            return isAvailable
                ? ValidationResult.Success
                : new ValidationResult("اسم المستخدم محجوز لمستخدم آخر.");
        }

        public static ValidationResult? ValidateUsername(string username, ValidationContext context)
        {
            var regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            return regex.IsMatch(username)
                ? ValidationResult.Success
                : new ValidationResult("اسم المستخدم يجب أن يبدأ بحرف لاتيني ويتبعه أرقام أو حروف فقط.");
        }
    }

}
