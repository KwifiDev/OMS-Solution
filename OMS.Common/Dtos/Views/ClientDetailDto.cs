using OMS.Common.Enums;

namespace OMS.Common.Dtos.Views;

public partial class ClientDetailDto
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string? Phone { get; set; }

    public EnClientType ClientType { get; set; }
}
