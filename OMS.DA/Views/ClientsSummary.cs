using Microsoft.EntityFrameworkCore;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views
{
    [Keyless]
    public class ClientsSummary
    {
        [Id]
        public int ClientId { get; set; }

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
