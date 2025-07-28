using System.ComponentModel.DataAnnotations;

namespace OMS.UI.APIs.Dtos.Tables
{
    public class RoleDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
