using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views;

public partial class UserAccountModel : IModelKey
{
    public int Id { get; set; }

    public string UserAccount1 { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string ClientType { get; set; } = null!;

    public decimal ClientBalance { get; set; }
}
