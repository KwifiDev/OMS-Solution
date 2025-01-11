﻿using OMS.DA.Enums;

namespace OMS.BL.Models.Tables;

public partial class DebtModel
{
    public int DebtId { get; internal set; }

    public required int ClientId { get; set; }

    public required int ServiceId { get; set; }

    public decimal Cost { get; set; }

    public required short Quantity { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? AmountDeducted { get; set; }

    public decimal? Total { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public DateOnly CreatedAt { get; internal set; }

    /// <summary>
    /// 0 = NotPaid | 1 = Paid | 2 = Canceled
    /// </summary>
    public EnDebtStatus Status { get; set; } = EnDebtStatus.NotPaid;

    public int? PaymentId { get; set; }

    public required int CreatedByUserId { get; set; }

    // ============================================================
    public string StatusText
    {
        get => Status == EnDebtStatus.NotPaid ? "غير مدفوع" :
                Status == EnDebtStatus.Paid ? "مدفوع" :
                Status == EnDebtStatus.Canceled ? "ملغا" :
                "غير معرف";
    }
}
