using OMS.API.Dtos.Tables;

namespace OMS.API.Dtos.Hybrid
{
    public class ClientAccountDto
    {
        public ClientDto Client { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;
    }
}
