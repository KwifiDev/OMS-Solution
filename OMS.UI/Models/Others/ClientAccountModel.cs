using OMS.UI.Models.Tables;

namespace OMS.UI.Models.Others
{
    public class ClientAccountModel
    {
        public ClientModel Client { get; set; }
        public AccountModel Account { get; set; }

        public ClientAccountModel(ClientModel clientModel, AccountModel accountModel)
        {
            Client = clientModel;
            Account = accountModel;
        }
    }
}
