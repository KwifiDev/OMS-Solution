using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class fixServicesSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW ServicesSummary AS
                                   SELECT SE.ServiceId, SE.Name, SE.Price,
								       ISNULL(SalesCount.TotalSales, 0) + ISNULL(DebtsCount.TotalDebts, 0) AS TotalConsumed
								   FROM Services SE
								   LEFT JOIN (
								       SELECT ServiceId, COUNT(*) AS TotalSales
								       FROM Sales
								       GROUP BY ServiceId
								   ) AS SalesCount ON SalesCount.ServiceId = SE.ServiceId
								   LEFT JOIN (
								       SELECT ServiceId, COUNT(*) AS TotalDebts
								       FROM Debts
								       GROUP BY ServiceId
								   ) AS DebtsCount ON DebtsCount.ServiceId = SE.ServiceId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW ServicesSummary AS
                                   SELECT SE.ServiceId, SE.Name, SE.Price, (COUNT(SA.SaleId) + COUNT(DE.DebtId)) AS TotalConsumed FROM Services SE
                                   LEFT JOIN Sales SA ON SE.ServiceId = SA.ServiceId
                                   LEFT JOIN Debts DE ON SE.ServiceId = DE.ServiceId
                                   GROUP BY SE.ServiceId, SE.Name, SE.Price");
        }
    }
}
