using System.Security.Cryptography;
using System.Text;

namespace OMS.UI.Services.Hash
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
