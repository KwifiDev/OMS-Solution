using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class ClientModel : BaseModel
    {

        private int _clientId;
        private int _personId;
        private EnClientType _clientType;

        [Key]
        public int ClientId
        {
            get => _clientId;
            set => SetProperty(ref _clientId, value);
        }

        public int PersonId
        {
            get => _personId;
            set => SetProperty(ref _personId, value);
        }

        public EnClientType ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }
    }
}
