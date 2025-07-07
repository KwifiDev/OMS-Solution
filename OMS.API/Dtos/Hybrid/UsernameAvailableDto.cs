using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Hybrid
{
    public class UsernameAvailableDto
    {
        public int UserId { get; set; }

        [MinLength(3, ErrorMessage = "المستخدم يجب ان يكون على الاقل ثلاث محارف")]
        public string Username { get; set; } = null!;
    }
}
