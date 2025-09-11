using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.CustomAttributes;
using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.DA.Views
{
    [Keyless]
    public class PersonDetail : IEntityKey
    {
        [Id]
        [Column("PersonId")]
        public int Id { get; set; }

        [StringLength(41)]
        public string FullName { get; set; } = null!;

        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }

        /// <summary>
        /// 0 = Male | 1 = Female
        /// </summary>
        public EnGender Gender { get; set; }
    }
}
