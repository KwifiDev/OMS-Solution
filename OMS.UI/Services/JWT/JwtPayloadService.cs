using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace OMS.UI.Services.JWT
{
    public class JwtPayloadService : IJwtPayloadService
    {
        public Dictionary<string, string> DecodePayload(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                    throw new Exception("Token format is invalid");

                var payload = parts[1];
                var payloadJson = Base64UrlDecode(payload);

                var payloadData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(payloadJson);

                var result = new Dictionary<string, string>();
                if (payloadData != null)
                {
                    foreach (var item in payloadData)
                    {
                        result[item.Key] = item.Value.ToString();
                    }
                }

                return result;
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        public string GetClaim(string token, string claimName)
        {
            var payload = DecodePayload(token);
            return payload.GetValueOrDefault(claimName, string.Empty);
        }

        public string GetUserId(string token)
        {
            return GetClaim(token, "sub") ?? string.Empty;
        }

        public List<string> GetRoles(string token)
        {
            var rolesString = GetClaim(token, ClaimTypes.Role);
            if (string.IsNullOrEmpty(rolesString))  return new List<string>();

            rolesString = rolesString.Replace("[", "")
                                     .Replace("]", "")
                                     .Replace("\"", "");

            return new List<string>(rolesString.Split(','));
        }

        public bool IsTokenExpired(string token)
        {
            var expString = GetClaim(token, "exp");
            if (string.IsNullOrEmpty(expString))
                return true;

            if (long.TryParse(expString, out var expTimestamp))
            {
                var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;
                return expDateTime <= DateTime.UtcNow;
            }

            return true;
        }

        private string Base64UrlDecode(string base64Url)
        {
            var base64 = base64Url.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
