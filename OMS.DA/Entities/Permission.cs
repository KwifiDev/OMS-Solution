using OMS.DA.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OMS.DA.Entities
{
    public class Permission : IEntityKey
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
