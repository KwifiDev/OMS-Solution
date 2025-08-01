﻿using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;

public partial class UserModel
{
    [Key]
    public int UserId { get; internal set; }

    public required int PersonId { get; set; }

    public required int BranchId { get; set; }

    public required string UserName { get; set; }

    public required string PasswordHash { get; set; }

    public bool IsActive { get; set; } = false;
}
