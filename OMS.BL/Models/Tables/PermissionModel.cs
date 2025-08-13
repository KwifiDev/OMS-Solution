using System.ComponentModel.DataAnnotations;

namespace OMS.BL.Models.Tables
{
    public class PermissionModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
