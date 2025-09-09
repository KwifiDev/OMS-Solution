namespace OMS.Common.Dtos.Hybrid
{
    public class LoginInfoDto
    {
        public ResponseLoginDto UserLogin { get; set; } = null!;
        public TokenDto TokenInfo { get; set; } = null!;
        public IEnumerable<string>? Claims { get; set; }
    }
}
