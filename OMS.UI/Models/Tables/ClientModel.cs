using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Tables
{
    public class ClientModel : BaseModel
    {

        private int _id;
        private int _personId;
        private EnClientType _clientType;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int PersonId
        {
            get => _personId;
            set => SetProperty(ref _personId, value);
        }

        [Required]
        [Range(0, 2, ErrorMessage = "الرجاء تحديد نوع العميل")]
        public EnClientType ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }
    }
}
