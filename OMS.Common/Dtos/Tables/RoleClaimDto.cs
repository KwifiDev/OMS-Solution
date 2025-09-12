using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables
{
    public class RoleClaimDto
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string? ClaimType { get; set; }

        public string? ClaimValue { get; set; }
    }
}
