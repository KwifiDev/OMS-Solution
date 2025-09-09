namespace OMS.Common.Dtos.Hybrid
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string TokenType { get; set; } = null!;
    }
}
