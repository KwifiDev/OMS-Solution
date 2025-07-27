using System.ComponentModel.DataAnnotations;

namespace OMS.UI.Models.Others
{
    public class ServiceOptionModel
    {
        [Key]
        public int ServiceId { get; set; }

        public string Name { get; set; } = null!;
    }
}
