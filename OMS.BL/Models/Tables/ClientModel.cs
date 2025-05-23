﻿using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class ClientModel
{
    [Key]
    public int ClientId { get; internal set; }

    public required int PersonId { get; set; }

    /// <summary>
    /// 0 = Normal | 1 = Lawyer | 2 = Other
    /// </summary>
    public required EnClientType ClientType { get; set; }
}
