namespace OMS.BL.Models.Hybrid
{
    public class TokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string TokenType { get; set; } = null!;
    }
}
