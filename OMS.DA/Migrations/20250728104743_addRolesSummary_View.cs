using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class addRolesSummary_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW RolesSummary AS
                                   SELECT 
                                       R.Id AS RoleId,
                                       R.Name AS RoleName,
                                       COUNT(DISTINCT UR.UserId) AS UsersCount,
                                       (SELECT COUNT(*) FROM Users) AS TotalUsers,
                                       CAST(COUNT(DISTINCT UR.UserId) AS FLOAT) / NULLIF((SELECT COUNT(*) FROM Users), 0) * 100 AS PercentageOfUsers,
                                       COUNT(DISTINCT RC.Id) AS ClaimsCount,
                                       (
                                           SELECT STRING_AGG(ClaimType, ', ')
                                           FROM (
                                               SELECT DISTINCT ClaimType
                                               FROM RoleClaims
                                               WHERE RoleId = R.Id
                                           ) AS DistinctClaims
                                       ) AS ClaimTypes,
                                       (
                                           SELECT STRING_AGG(ClaimValue, ', ')
                                           FROM (
                                               SELECT DISTINCT ClaimValue
                                               FROM RoleClaims
                                               WHERE RoleId = R.Id
                                           ) AS DistinctClaimValues
                                       ) AS ClaimValues
                                   FROM 
                                       Roles R
                                   LEFT JOIN 
                                       UserRoles UR ON R.Id = UR.RoleId
                                   LEFT JOIN
                                       RoleClaims RC ON R.Id = RC.RoleId
                                   GROUP BY 
                                       R.Id, R.Name;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW RolesSummary");
        }
    }
}
