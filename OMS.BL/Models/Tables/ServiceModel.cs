﻿using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables;


public partial class ServiceModel
{
    [Key]
    public int ServiceId { get; internal set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required decimal Price { get; set; }
}
