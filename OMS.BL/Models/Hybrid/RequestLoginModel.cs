using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Hybrid
{
    public class RequestLoginModel
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
