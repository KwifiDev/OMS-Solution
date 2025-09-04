using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views
{
    [Keyless]
    public class ClientsSummary : IEntityKey
    {
        [Id]
        [Column("ClientId")]
        public int Id { get; set; }

        public int? AccountId { get; set; }

        [StringLength(41)]
        public string FullName { get; set; } = null!;

        [StringLength(15)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;

        [StringLength(8)]
        [Unicode(false)]
        public string ClientType { get; set; } = null!;
    }
}
