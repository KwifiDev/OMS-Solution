using System.ComponentModel.DataAnnotations;

namespace OMS.API.Dtos.Tables
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Role Name Must be at least 3 char")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "only English charecters")]
        public string Name { get; set; } = null!;
    }
}
