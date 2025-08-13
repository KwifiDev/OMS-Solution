using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
