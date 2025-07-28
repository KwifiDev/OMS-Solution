namespace OMS.UI.APIs.Dtos.Views
{
    public class RolesSummaryDto
    {
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public int? UsersCount { get; set; }

        public int? TotalUsers { get; set; }

        public double? PercentageOfUsers { get; set; }

        public int? ClaimsCount { get; set; }

        public string? ClaimTypes { get; set; }

        public string? ClaimValues { get; set; }
    }
}
