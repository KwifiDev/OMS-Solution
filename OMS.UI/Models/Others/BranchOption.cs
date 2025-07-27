using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Others
{
    public class BranchOptionModel
    {
        [Key]
        public int BranchId { get; set; }

        public string Name { get; set; } = null!;
    }
}
