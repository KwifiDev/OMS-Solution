using OMS.UI.APIs.Dtos.Tables;

namespace OMS.UI.APIs.Dtos.Hybrid
{
    public class ClientAccountDto
    {
        public ClientDto Client { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;
    }
}
