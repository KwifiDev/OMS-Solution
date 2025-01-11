namespace OMS.BL.Models.Views;

public partial class UserAccountModel
{
    public int AccountId { get; set; }

    public string UserAccount1 { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string ClientType { get; set; } = null!;

    public string? ClientBalance { get; set; }
}
