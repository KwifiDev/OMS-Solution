using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS.DA.Migrations
{
    /// <inheritdoc />
    public partial class editClientsSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientsSummary] AS
                                   SELECT C.ClientId, 
                                   	      A.AccountId,
                                   	      FullName = P.FirstName + ' ' + P.LastName,
                                   	      P.Phone,
                                   	      C.ClientType,
										  SUM(ISNULL(D.Total, 0)) AS TotalDebts
                                   	      FROM Clients C INNER JOIN People P
                                   	      ON C.PersonId = P.PersonId LEFT JOIN Accounts A
                                   	      ON A.ClientId = C.ClientId LEFT JOIN Debts D
										  ON D.ClientId = C.ClientId AND D.Status = 0
										  GROUP BY C.ClientId, 
												   A.AccountId,
												   P.FirstName,
												   P.LastName,
												   P.Phone,
												   C.ClientType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientsSummary] AS
                                   SELECT C.ClientId, 
                                   	      A.AccountId,
                                   	      FullName = P.FirstName + ' ' + P.LastName,
                                   	      P.Phone,
                                   	      C.ClientType
                                   	      FROM Clients C INNER JOIN People P
                                   	      ON C.PersonId = P.PersonId LEFT JOIN Accounts A
                                   	      ON A.ClientId = C.ClientId");
        }
    }
}
