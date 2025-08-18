using OMS.UI.Models.Others;

namespace OMS.UI.APIs.Dtos.Hybrid
{
    public class LoginInfoDto
    {
        public ResponseLoginDto UserLogin { get; set; } = null!;
        public TokenModel TokenInfo { get; set; } = null!;
        public IEnumerable<string>? Claims { get; set; }
    }
}
