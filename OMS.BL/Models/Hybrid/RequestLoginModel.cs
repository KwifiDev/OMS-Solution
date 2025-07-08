using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Hybrid
{
    public class RequestLoginModel
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
