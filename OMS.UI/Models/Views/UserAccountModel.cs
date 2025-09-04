using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Views
{
    public class UserAccountModel : BaseModel
    {

        private int _id;
        private string _userAccount1 = null!;
        private string _clientName = null!;
        private string _clientType = null!;
        private decimal _clientBalance;

        [Key]
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string UserAccount1
        {
            get => _userAccount1;
            set => SetProperty(ref _userAccount1, value);
        }

        public string ClientName
        {
            get => _clientName;
            set => SetProperty(ref _clientName, value);
        }

        public string ClientType
        {
            get => _clientType;
            set => SetProperty(ref _clientType, value);
        }

        public decimal ClientBalance
        {
            get => _clientBalance;
            set => SetProperty(ref _clientBalance, value);
        }
    }
}
