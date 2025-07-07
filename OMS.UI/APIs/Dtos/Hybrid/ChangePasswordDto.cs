namespace OMS.UI.APIs.Dtos.Hybrid
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;

    }
}
