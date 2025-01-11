using OMS.DA.Enums;

namespace OMS.BL.Models.Tables;

public partial class DiscountModel
{
    public int DiscountId { get; internal set; }

    public required int ServiceId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }

    public required decimal DiscountPercentage { get; set; }

    // ===========================================================
    public string ClientTypeText
    {
        get => ClientType == EnClientType.Normal ? "عادي" :
                ClientType == EnClientType.Lawyer ? "محامي" :
                ClientType == EnClientType.Other ? "غير ذلك" :
                "غير معروف";
    }
}
