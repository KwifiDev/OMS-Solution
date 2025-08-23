namespace OMS.UI.Services.JWT
{
    public interface IJwtPayloadService
    {
        Dictionary<string, string> DecodePayload(string token);
        string GetClaim(string token, string claimName);
        string GetUserId(string token);
        List<string> GetRoles(string token);
        bool IsTokenExpired(string token);
    }
}
