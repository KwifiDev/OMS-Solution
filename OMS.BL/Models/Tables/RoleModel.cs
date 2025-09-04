using OMS.BL.Interfaces;

namespace OMS.BL.Models.Tables
{
    public class RoleModel : IModelKey
    {
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}
