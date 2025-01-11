namespace OMS.BL.Models.Views;

public partial class ClientsByTypeModel
{
    public string ClientType { get; set; } = null!;

    public int? TotalClients { get; set; }
}
