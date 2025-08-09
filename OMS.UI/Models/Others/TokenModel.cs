namespace OMS.UI.Models.Others
{
    public class TokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string TokenType { get; set; } = null!;
    }
}
