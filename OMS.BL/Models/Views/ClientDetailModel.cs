namespace OMS.BL.Models.Views;

public partial class ClientDetailModel
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ClientType { get; set; } = null!;
}
