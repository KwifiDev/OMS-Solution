using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS.DA.Migrations
{
    /// <inheritdoc />
    public partial class EditViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[AccountBalancesTransactions] AS
                                   SELECT A.AccountId,
                                   		ClientName = (C.FirstName + ' ' + C.LastName),
                                   		A.UserAccount, 
                                   		A.Balance AS AccountBalance, 
                                   		COUNT(T.TransactionId) AS TotalTransactions, SUM(T.Amount) AS TotalTransactionAmount
                                   FROM Accounts A
                                   JOIN Clients CL ON A.ClientId = CL.ClientId
                                   JOIN People C ON CL.PersonId = C.PersonId
                                   LEFT JOIN Transactions T ON A.AccountId = T.AccountId
                                   WHERE A.Balance > 0
                                   GROUP BY A.AccountId, C.FirstName, C.LastName, A.UserAccount, A.Balance;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientDetails] AS
                                   SELECT C.ClientId,
                                   	ClientName = (P.FirstName + ' ' + P.LastName),
                                   	P.Phone, 
                                   	C.ClientType
                                   FROM Clients C
                                   JOIN People P ON C.PersonId = P.PersonId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientsSummary] AS
                                   SELECT C.ClientId, 
                                   	      A.AccountId,
                                   	      FullName = P.FirstName + ' ' + P.LastName,
                                   	      P.Phone,
                                   	      C.ClientType
                                   	      FROM Clients C INNER JOIN People P
                                   	      ON C.PersonId = P.PersonId LEFT JOIN Accounts A
                                   	      ON A.ClientId = C.ClientId");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[DebtsSummary] AS
										SELECT D.DebtId,
											   CL.ClientId,
											   S.Name AS ServiceName,
											   D.Description,
											   D.Notes,
											   D.Total AS TotalDebts, 
											   D.Status ,
											   D.CreatedAt
										FROM Debts D INNER JOIN Clients CL
										ON D.ClientId = CL.ClientId INNER JOIN People C 
										ON CL.PersonId = C.PersonId INNER JOIN Services S 
										ON D.ServiceId = S.ServiceId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[DiscountsApplied] AS
                                   SELECT D.DiscountId,
                                   	      S.ServiceId,
                                   	      S.Name AS ServiceName,
                                   	      S.Price AS ServicePrice,
                                   	      D .ClientType,
                                   	      D.DiscountPercentage
                                   FROM dbo.Discounts AS D INNER JOIN dbo.Services AS S
                                       ON D.ServiceId = S.ServiceId");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[PaymentsSummary] AS
                                   SELECT P.PaymentId,
                                   	   A.AccountId,
                                   	   AmountPaid = P.Amount,
                                   	   P.CreatedAt,
                                   	   P.Notes,
                                   	   (E.FirstName + ' ' + E.LastName) AS EmployeeName
                                   FROM Payments P
                                   JOIN Users U ON P.CreatedByUserId = U.UserId
                                   JOIN Accounts A ON P.AccountId = A.AccountId
                                   JOIN People E ON U.PersonId = E.PersonId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[PersonDetails] AS
                                   SELECT PersonId,
                                   	      FullName = (FirstName + ' ' + LastName),
                                   	      Phone,
                                          Gender
                                   FROM People;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[SalesSummary] AS
										SELECT D.SaleId, 
										 	   CL.ClientId,
										 	   S.Name AS ServiceName,
										 	   D.Description,
											   D.Notes,
										 	   TotalSales = D.Total,
										 	   D.Status,
											   D.CreatedAt
										FROM Sales D
										JOIN Clients CL ON D.ClientId = CL.ClientId
										JOIN People C ON CL.PersonId = C.PersonId
										JOIN Services S ON D.ServiceId = S.ServiceId;");

            migrationBuilder.Sql(@"ALTER View [dbo].[TransactionsSummary] AS
                                   SELECT T.TransactionId,
                                          A.AccountId,
                                          T.TransactionType,
                                          T.Amount,
                                          T.CreatedAt,
                                          T.Notes
                                          FROM dbo.Transactions AS T INNER JOIN dbo.Accounts AS A ON T.AccountId = A.AccountId");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserAccounts] AS
                                   SELECT A.AccountId,
                                   	    A.UserAccount,
                                   	    ClientName = (C.FirstName + ' ' + C.LastName),
                                   	    CL.ClientType,
                                   	    A.Balance AS ClientBalance
                                   FROM Accounts A
                                   JOIN Clients CL ON A.ClientId = CL.ClientId
                                   JOIN People C ON CL.PersonId = C.PersonId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserDetails] AS
                                   SELECT 
                                       U.UserId, 
                                       EmployeeName = (P.FirstName + ' ' + P.LastName),
                                       U.Username, 
                                       U.IsActive, 
                                       B.Name AS WorkingBranch
                                   FROM 
                                       Users U
                                   JOIN 
                                       People P ON U.PersonId = P.PersonId
                                   JOIN 
                                       Branches B ON U.BranchId = B.BranchId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW [dbo].[AccountBalancesTransactions] AS
                                   SELECT A.AccountId,
                                   		ClientName = (C.FirstName + ' ' + C.LastName),
                                   		A.UserAccount, (CAST(A.Balance AS varchar(15)) + ' L.S') AS AccountBalance, 
                                   		COUNT(T.TransactionId) AS TotalTransactions, SUM(T.Amount) AS TotalTransactionAmount
                                   FROM Accounts A
                                   JOIN Clients CL ON A.ClientId = CL.ClientId
                                   JOIN People C ON CL.PersonId = C.PersonId
                                   LEFT JOIN Transactions T ON A.AccountId = T.AccountId
                                   WHERE A.Balance > 0
                                   GROUP BY A.AccountId, C.FirstName, C.LastName, A.UserAccount, A.Balance;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientDetails] AS
                                   SELECT C.ClientId,
                                   	ClientName = (P.FirstName + ' ' + P.LastName),
                                   	ISNULL(P.Phone, 'لا يوجد رقم هاتف') AS Phone, 
                                   	CASE C.ClientType
                                   		WHEN 0 THEN 'عادي'
                                   		WHEN 1 THEN 'محامي'
                                   		WHEN 2 THEN 'غير ذلك'
                                   		ELSE 'غير معرف'
                                   		END AS ClientType
                                   FROM Clients C
                                   JOIN People P ON C.PersonId = P.PersonId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[ClientsSummary] AS
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

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[DebtsSummary] AS
										SELECT D.DebtId,
											   CL.ClientId,
											   S.Name AS ServiceName,
											   ISNULL(D.Description, 'لا يوجد وصف') AS Description,
											   ISNULL(D.Notes, 'لا يوجد ملاحظات') AS Notes,
											   D.Total AS TotalDebts, 
											   CASE D.Status 
											   WHEN 0 THEN 'غير مدفوع' 
											   WHEN 1 THEN 'مدفوع' 
											   WHEN 2 THEN 'ملغات' 
											   ELSE 'غير معرف' END AS Status,
											   D.CreatedAt
										FROM Debts D INNER JOIN Clients CL
										ON D.ClientId = CL.ClientId INNER JOIN People C 
										ON CL.PersonId = C.PersonId INNER JOIN Services S 
										ON D.ServiceId = S.ServiceId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[DiscountsApplied] AS
                                   SELECT D.DiscountId,
                                   	      S.ServiceId,
                                   	      S.Name AS ServiceName,
                                   	      S.Price AS ServicePrice,
                                   	      CASE D .ClientType 
                                   	      WHEN 0 THEN 'عادي' 
                                   	      WHEN 1 THEN 'محامي' 
                                   	      WHEN 2 THEN 'غير ذلك'
                                   	      ELSE 'غير معرف' END AS ClientType, 
                                   	      CAST(D.DiscountPercentage AS varchar(10)) + '%' AS Discount
                                   FROM dbo.Discounts AS D INNER JOIN dbo.Services AS S
                                       ON D.ServiceId = S.ServiceId");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[PaymentsSummary] AS
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

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[PersonDetails] AS
                                   SELECT PersonId,
                                   	      FullName = (FirstName + ' ' + LastName),
                                   	      ISNULL(Phone, 'لا يوجد') AS Phone,
                                          Gender
                                   FROM People;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[SalesSummary] AS
										SELECT D.SaleId, 
										 	   CL.ClientId,
										 	   S.Name AS ServiceName,
										 	   ISNULL(D.Description, 'لا يوجد وصف') AS Description,
											   ISNULL(D.Notes, 'لا يوجد ملاحظات') AS Notes,
										 	   TotalSales = D.Total,
										 	   CASE D.Status
										 	   WHEN 0 THEN 'غير مكتملة'
										 	   WHEN 1 THEN 'مكتملة'
										 	   WHEN 2 THEN 'ملغات'
										 	   ELSE 'غير معرف' 
										 	   END AS Status,
											   D.CreatedAt
										FROM Sales D
										JOIN Clients CL ON D.ClientId = CL.ClientId
										JOIN People C ON CL.PersonId = C.PersonId
										JOIN Services S ON D.ServiceId = S.ServiceId;");

            migrationBuilder.Sql(@"ALTER View [dbo].[TransactionsSummary] AS
                                   SELECT T.TransactionId,
                                          A.AccountId,
                                          CASE T .TransactionType
                                          WHEN 0 THEN 'أيداع'
                                          WHEN 1 THEN 'سحب'
                                          WHEN 2 THEN 'تحويل'
                                          ELSE 'غير معرف'
                                          END AS TransactionType,
                                          CAST(T.Amount AS varchar(15)) + ' L.S' AS Amount, 
                                          T.CreatedAt,
                                          ISNULL(T.Notes, 'لا يوجد ملاحظات') AS Notes
                                          FROM dbo.Transactions AS T INNER JOIN dbo.Accounts AS A ON T.AccountId = A.AccountId");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserAccounts] AS
                                   SELECT A.AccountId,
                                   	    A.UserAccount,
                                   	    ClientName = (C.FirstName + ' ' + C.LastName),
                                   	    CASE CL.ClientType
                                   	    WHEN 0 THEN 'عادي'
                                   	    WHEN 1 THEN 'محامي'
                                   	    WHEN 2 THEN 'غير ذلك'
                                   	    ELSE 'غير معرف'
                                   	    END AS ClientType,
                                   	    A.Balance AS ClientBalance
                                   FROM Accounts A
                                   JOIN Clients CL ON A.ClientId = CL.ClientId
                                   JOIN People C ON CL.PersonId = C.PersonId;");

            migrationBuilder.Sql(@"ALTER VIEW [dbo].[UserDetails] AS
                                   SELECT 
                                       U.UserId, 
                                       EmployeeName = (P.FirstName + ' ' + P.LastName),
                                       U.Username, 
                                       CASE U.IsActive
                                   		WHEN 0 THEN 'غير فعّال'
                                   		WHEN 1 THEN 'فعّال'
                                   		END AS IsActive, 
                                       B.Name AS WorkingBranch
                                   FROM 
                                       Users U
                                   JOIN 
                                       People P ON U.PersonId = P.PersonId
                                   JOIN 
                                       Branches B ON U.BranchId = B.BranchId;");

        }
    }
}
