using OMS.Common.Enums;

namespace OMS.UI.APIServices.Dtos.Views
{
    public class PersonDetailDto
    {
        public int PersonId { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        /// <summary>
        /// 0 = Male | 1 = Female
        /// </summary>
        public EnGender Gender { get; set; }
    }
}
