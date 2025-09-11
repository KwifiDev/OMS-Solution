using OMS.BL.Interfaces;
using OMS.Common.Enums;

namespace OMS.BL.Models.Views;

public partial class UserAccountModel : IModelKey
{
    public int Id { get; set; }

    public string UserAccount1 { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public EnClientType ClientType { get; set; }

    public decimal ClientBalance { get; set; }
}
