using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OMS.UI.Models.Validations
{
    public class UserValidation
    {
        public static ValidationResult? ValidateUsername(string username, ValidationContext context)
        {
            var regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            return regex.IsMatch(username)
                ? ValidationResult.Success
                : new ValidationResult("اسم المستخدم يجب أن يبدأ على الاقل بحرف[EN].");
        }
    }
}
