using OMS.DA.Enums;

namespace OMS.BL.Dtos.Tables;

public partial class ClientDto
{
    public int ClientId { get; internal set; }

    public required int PersonId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }

    // =============================================================
    public string ClientTypeText
    {
        get => ClientType == EnClientType.Normal ? "عادي" :
                ClientType == EnClientType.Lawyer ? "محامي" :
                ClientType == EnClientType.Other ? "غير ذلك" :
                "غير معروف";
    }
}
