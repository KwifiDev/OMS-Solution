using OMS.DA.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace OMS.BL.Services.Security
{
    public interface ITokenService
    {
        Task<JwtSecurityToken?> GenerateToken(User user);
    }
}
