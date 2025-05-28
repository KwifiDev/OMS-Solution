using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class addServicesSummary_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW ServicesSummary AS
                                   SELECT SE.ServiceId, SE.Name, SE.Price, (COUNT(SA.SaleId) + COUNT(DE.DebtId)) AS TotalConsumed FROM Services SE
                                   LEFT JOIN Sales SA ON SE.ServiceId = SA.ServiceId
                                   LEFT JOIN Debts DE ON SE.ServiceId = DE.ServiceId
                                   GROUP BY SE.ServiceId, SE.Name, SE.Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW ServicesSummary");
        }
    }
}
