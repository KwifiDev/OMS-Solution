﻿using OMS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class PersonModel
{
    [Key]
    public int PersonId { get; internal set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    /// <summary>
    /// 0 = Male | 1 = Female
    /// </summary>
    public required EnGender Gender { get; set; }

    public string? Phone { get; set; }

}
