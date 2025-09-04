using Microsoft.AspNetCore.Identity;
using OMS.DA.Interfaces;

namespace OMS.DA.Entities.Identity
{
    public partial class Role : IdentityRole<int>, IEntityKey
    {

    }
}
