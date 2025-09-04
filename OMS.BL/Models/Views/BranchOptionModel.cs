using OMS.BL.Interfaces;

namespace OMS.BL.Models.Views
{
    public partial class BranchOptionModel : IModelKey
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
