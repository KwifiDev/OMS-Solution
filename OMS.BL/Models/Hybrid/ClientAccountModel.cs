using OMS.BL.Models.Tables;

namespace OMS.BL.Models.Hybrid
{
    public class ClientAccountModel
    {
        public ClientModel Client { get; set; } = null!;
        public AccountModel Account { get; set; } = null!;
    }
}
