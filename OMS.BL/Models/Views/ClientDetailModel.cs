using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class ClientDetailModel : IModelKey
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ClientType { get; set; } = null!;
}
