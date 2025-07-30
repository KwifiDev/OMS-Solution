namespace OMS.API.Dtos.Hybrid
{
    public class InputUserRolesDto
    {
        public int UserId { get; set; }
        public ICollection<string> RolesToAdd { get; set; } = [];
        public ICollection<string> RolesToRemove { get; set; } = [];
    }
}
