using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class AddClientsSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW ClientsSummary AS
                                   SELECT C.ClientId, 
                                   	      A.AccountId,
                                   	      FullName = P.FirstName + ' ' + P.LastName,
                                   	      ISNULL(P.Phone, 'لا يوجد هاتف') AS Phone,
                                   	      CASE C.ClientType
                                   	      WHEN 0 THEN 'عادي'
                                   	      WHEN 1 THEN 'محامي'
                                   	      ELSE 'أخر' END AS ClientType
                                   	      FROM Clients C INNER JOIN People P
                                   	      ON C.PersonId = P.PersonId LEFT JOIN Accounts A
                                   	      ON A.ClientId = C.ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW ClientsSummary");
        }
    }
}
