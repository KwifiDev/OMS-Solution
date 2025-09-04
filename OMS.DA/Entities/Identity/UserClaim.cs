using Microsoft.AspNetCore.Identity;
using OMS.DA.Interfaces;

namespace OMS.DA.Entities.Identity
{
    public partial class UserClaim : IdentityUserClaim<int>, IEntityKey
    {
    }
}
