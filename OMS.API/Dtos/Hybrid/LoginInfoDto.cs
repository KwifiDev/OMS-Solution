using OMS.BL.Models.Hybrid;

namespace OMS.API.Dtos.Hybrid
{
    public class LoginInfoDto
    {
        public ResponseLoginDto UserLogin { get; set; } = null!;
        public TokenModel TokenInfo { get; set; } = null!;
    }
}
