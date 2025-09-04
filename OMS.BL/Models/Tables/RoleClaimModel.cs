using OMS.BL.Interfaces;

namespace OMS.BL.Models.Tables
{
    public class RoleClaimModel : IModelKey
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string? ClaimType { get; set; }

        public string? ClaimValue { get; set; }
    }
}
