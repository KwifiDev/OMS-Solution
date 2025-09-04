using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views
{
    [Keyless]
    public class RolesSummary : IEntityKey
    {
        [Id]
        [Column("RoleId")]
        public int Id { get; set; }

        [StringLength(256)]
        public string? RoleName { get; set; }

        public int? UsersCount { get; set; }

        public int? TotalUsers { get; set; }

        public double? PercentageOfUsers { get; set; }

        public int? ClaimsCount { get; set; }

        public string? ClaimTypes { get; set; }

        public string? ClaimValues { get; set; }

    }
}
