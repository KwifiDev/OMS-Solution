using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class ClientDetailModel : IModelKey
{
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;

    public string? Phone { get; set; }

    public EnClientType ClientType { get; set; }
}
