using OMS.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables
{
    public class PermissionModel : IModelKey
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
