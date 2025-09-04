using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views
{
    public class PersonDetailModel : IModelKey
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        /// <summary>
        /// 0 = Male | 1 = Female
        /// </summary>
        public EnGender Gender { get; set; }
    }
}
