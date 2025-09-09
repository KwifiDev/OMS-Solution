using OMS.Common.Dtos.Tables;

namespace OMS.Common.Dtos.Hybrid
{
    public class ClientAccountDto
    {
        public ClientDto Client { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;
    }
}
