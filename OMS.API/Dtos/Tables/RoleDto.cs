using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Role Name Must be at least 3 char")]
        public string? Name { get; set; }
    }
}
