using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class editPaymentsSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW PaymentsSummary AS
                                    SELECT P.PaymentId,
                                    	   A.AccountId,
                                    	   AmountPaid = P.Amount,
                                    	   P.CreatedAt,
                                    	   ISNULL(P.Notes, 'لا يوجد ملاحظات') AS Notes,
                                    	   (E.FirstName + ' ' + E.LastName) AS EmployeeName
                                    FROM Payments P
                                    JOIN Users U ON P.CreatedByUserId = U.UserId
                                    JOIN Accounts A ON P.AccountId = A.AccountId
                                    JOIN People E ON U.PersonId = E.PersonId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW PaymentsSummary AS
                                    SELECT P.PaymentId,
                                    	   ClientName =(E.FirstName + ' ' + E.LastName),
                                    	   AmountPaid = (CAST(P.Amount AS varchar(15)) + ' L.S'),
                                    	   P.CreatedAt,
                                    	   ISNULL(P.Notes, 'لا يوجد ملاحظات') AS Notes
                                    FROM Payments P
                                    JOIN Accounts A ON P.AccountId = A.AccountId
                                    JOIN Clients C ON A.ClientId = C.ClientId
                                    JOIN People E ON C.PersonId = E.PersonId;");
        }
    }
}
