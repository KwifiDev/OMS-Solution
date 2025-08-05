using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Hybrid
{
    public class UsernameAvailableDto
    {
        public int UserId { get; set; }

        [MinLength(3, ErrorMessage = "username must be at least 3 char")]
        public string Username { get; set; } = null!;
    }
}
