namespace OMS.Common.Dtos.Views;

public partial class ClientsByTypeDto
{
    public string ClientType { get; set; } = null!;

    public int? TotalClients { get; set; }
}
