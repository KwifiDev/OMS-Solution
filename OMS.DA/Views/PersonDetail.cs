using Microsoft.EntityFrameworkCore;
using OMS.Common.Enums;
using OMS.DA.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Views
{
    [Keyless]
    public class PersonDetail
    {
        [Id]
        public int PersonId { get; set; }

        [StringLength(41)]
        public string FullName { get; set; } = null!;

        [StringLength(15)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;

        /// <summary>
        /// 0 = Male | 1 = Female
        /// </summary>
        public EnGender Gender { get; set; }
    }
}
