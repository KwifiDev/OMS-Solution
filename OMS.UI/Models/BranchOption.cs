using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models
{
    public class BranchOption
    {
        [Key]
        public int BranchId { get; set; }

        public string Name { get; set; } = null!;
    }
}
