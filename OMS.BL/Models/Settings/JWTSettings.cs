namespace OMS.BL.Models.Settings
{
    public class JWTSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public double ExpireMinutes { get; set; }
    }
}
