using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Tables
{
    public class PermissionDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "PermissionName must be at least 5 chars")]
        public string Name { get; set; } = null!;
    }
}
