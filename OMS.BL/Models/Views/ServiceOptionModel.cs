using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views
{
    public class ServiceOptionModel : IModelKey
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
