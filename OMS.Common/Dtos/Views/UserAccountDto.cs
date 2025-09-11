using OMS.Common.Enums;

namespace OMS.Common.Dtos.Views;

public partial class UserAccountDto
{
    public int Id { get; set; }

    public string UserAccount1 { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public EnClientType ClientType { get; set; }

    public decimal ClientBalance { get; set; }
}
