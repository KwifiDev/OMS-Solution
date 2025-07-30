namespace OMS.BL.Models.Hybrid
{
    public class InputUserRolesModel
    {
        public int UserId { get; set; }
        public ICollection<string> RolesToAdd { get; set; } = [];
        public ICollection<string> RolesToRemove { get; set; } = [];
    }
}
