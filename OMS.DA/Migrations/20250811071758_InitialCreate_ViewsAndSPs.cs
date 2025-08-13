using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS.DA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_ViewsAndSPs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[AccountBalancesTransactions] AS
                                   SELECT A.AccountId,
                                   		ClientName = (C.FirstName + ' ' + C.LastName),
                                   		A.UserAccount, (CAST(A.Balance AS varchar(15)) + ' L.S') AS AccountBalance, 
                                   		COUNT(T.TransactionId) AS TotalTransactions, SUM(T.Amount) AS TotalTransactionAmount
                                   FROM Accounts A
                                   JOIN Clients CL ON A.ClientId = CL.ClientId
                                   JOIN People C ON CL.PersonId = C.PersonId
                                   LEFT JOIN Transactions T ON A.AccountId = T.AccountId
                                   WHERE A.Balance > 0
                                   GROUP BY A.AccountId, C.FirstName, C.LastName, A.UserAccount, A.Balance;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[BranchOperationalMetrics] AS
                                   SELECT B.BranchId,
                                   	B.Name,
                                   	B.Address, 
                                       COUNT(U.UserId) AS TotalEmployees
                                   FROM Branches B
                                   LEFT JOIN Users U ON B.BranchId = U.BranchId
                                   GROUP BY B.BranchId, B.Name, B.Address;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[ClientDetails] AS
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
                                   JOIN People P ON C.PersonId = P.PersonId;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[ClientsByType] AS
                                   SELECT CASE ClientType
                                   	WHEN 0 THEN 'عادي'
                                   	WHEN 1 THEN 'محامي'
                                   	WHEN 2 THEN 'غير ذلك'
                                   	ELSE 'غير معرف'
                                   	END AS ClientType,
                                   	COUNT(*) AS TotalClients
                                   FROM Clients
                                   GROUP BY ClientType;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[ClientsSummary] AS
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
                                                                      	      ON A.ClientId = C.ClientId
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[DashboardSummary] AS
                                                                             WITH RevenueData AS (
                                                                                 SELECT SUM(Amount) AS TotalRevenues FROM Revenues
                                                                             ),
                                                                             TransactionData AS (
                                                                                 SELECT 
                                                                                     SUM(CASE WHEN TransactionType = 0 THEN Amount ELSE 0 END) AS Deposit,
                                                                                     SUM(CASE WHEN TransactionType = 1 THEN Amount ELSE 0 END) AS Withdraw
                                                                                 FROM Transactions
                                                                             ),
                                                                             AccountData AS (
                                                                                 SELECT SUM(Balance) AS TotalBalance FROM Accounts
                                                                             ),
                                                                             PaymentData AS (
                                                                                 SELECT SUM(Amount) AS TotalPayments FROM Payments
                                                                             ),
                                                                             SalesData AS (
                                                                                 SELECT 
                                                                                     SUM(Total) AS TotalSales,
                                                                                     SUM(AmountDeducted) AS TotalSalesAmountDeducted
                                                                                 FROM Sales 
                                                                                 WHERE Status != 2
                                                                             ),
                                                                             DebtData AS (
                                                                                 SELECT 
                                                                                     SUM(CASE WHEN Status = 0 THEN Total ELSE 0 END) AS NotPaidDebts,
                                                                                     SUM(CASE WHEN Status = 1 THEN Total ELSE 0 END) AS PaidDebts,
                                                                                     SUM(CASE WHEN Status = 1 THEN AmountDeducted ELSE 0 END) AS TotalDebtsAmountDeducted
                                                                                 FROM Debts
                                                                             )
                                                                             SELECT 
                                                                                 r.TotalRevenues,
                                                                                 t.Deposit,
                                                                                 t.Withdraw,
                                                                                 a.TotalBalance,
                                                                                 p.TotalPayments,
                                                                                 s.TotalSales,
                                                                                 s.TotalSalesAmountDeducted,
                                                                                 d.NotPaidDebts,
                                                                                 d.PaidDebts,
                                                                                 d.TotalDebtsAmountDeducted,
                                                                                 -- يمكن إضافة حسابات إضافية هنا مثل:
                                                                                 (r.TotalRevenues + t.Deposit - t.Withdraw) AS NetCashFlow,
                                                                                 (s.TotalSales + d.PaidDebts) AS TotalIncome
                                                                             FROM 
                                                                                 RevenueData r
                                                                                 CROSS JOIN TransactionData t
                                                                                 CROSS JOIN AccountData a
                                                                                 CROSS JOIN PaymentData p
                                                                                 CROSS JOIN SalesData s
                                                                                 CROSS JOIN DebtData d;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[DebtsByStatus] AS
                                   SELECT CASE Status
                                   	WHEN 0 THEN 'غير مدفوع'
                                   	WHEN 1 THEN 'مدفوع'
                                   	WHEN 2 THEN 'ملغات'
                                   	ELSE 'غير معرف' 
                                   	END AS DebtsStatus,
                                   	COUNT(*) AS TotalDebts,
                                   	(CAST(SUM(Total) AS varchar) + ' L.S') AS TotalAmount
                                   FROM Debts
                                   GROUP BY Status;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[DebtsSummary] AS
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
                                   			                                    	   ELSE 'غير معرف' END AS Status
                                   			                                    FROM Debts D INNER JOIN Clients CL
                                   			                                    ON D.ClientId = CL.ClientId INNER JOIN People C 
                                   			                                    ON CL.PersonId = C.PersonId INNER JOIN Services S 
                                   			                                    ON D.ServiceId = S.ServiceId;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[DiscountsApplied] AS
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
                                                                      FROM   dbo.Discounts AS D INNER JOIN dbo.Services AS S
                                   	                                      ON D.ServiceId = S.ServiceId
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[MonthlyFinancialSummary] AS
                                   SELECT YEAR(R.CreatedAt) AS Year, MONTH(R.CreatedAt) AS Month, 
                                          SUM(R.Amount) AS TotalRevenue
                                   FROM Revenues R
                                   GROUP BY YEAR(R.CreatedAt), MONTH(R.CreatedAt);
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[PaymentsSummary] AS
                                                                       SELECT P.PaymentId,
                                                                       	   A.AccountId,
                                                                       	   AmountPaid = P.Amount,
                                                                       	   P.CreatedAt,
                                                                       	   ISNULL(P.Notes, 'لا يوجد ملاحظات') AS Notes,
                                                                       	   (E.FirstName + ' ' + E.LastName) AS EmployeeName
                                                                       FROM Payments P
                                                                       JOIN Users U ON P.CreatedByUserId = U.UserId
                                                                       JOIN Accounts A ON P.AccountId = A.AccountId
                                                                       JOIN People E ON U.PersonId = E.PersonId;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[PersonDetails] AS
                                                                      SELECT PersonId,
                                                                      	      FullName = (FirstName + ' ' + LastName),
                                                                      	      ISNULL(Phone, 'لا يوجد') AS Phone,
                                                                             Gender
                                                                      FROM People;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[RolesSummary] AS
                                                                      SELECT 
                                                                          R.Id AS RoleId,
                                                                          R.Name AS RoleName,
                                                                          COUNT(DISTINCT UR.UserId) AS UsersCount,
                                                                          (SELECT COUNT(*) FROM Users) AS TotalUsers,
                                                                          ROUND(CAST(COUNT(DISTINCT UR.UserId) AS FLOAT) / NULLIF((SELECT COUNT(*) FROM Users), 0) * 100, 1) AS PercentageOfUsers,
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
                                                                          R.Id, R.Name;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[SalesSummary] AS
                                                                      SELECT  D.SaleId, 
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
                                                                       	   END AS Status
                                                                       FROM Sales D
                                                                       JOIN Clients CL ON D.ClientId = CL.ClientId
                                                                       JOIN People C ON CL.PersonId = C.PersonId
                                                                       JOIN Services S ON D.ServiceId = S.ServiceId;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[ServicesSummary] AS
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
                                   								   ) AS DebtsCount ON DebtsCount.ServiceId = SE.ServiceId;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[TransactionsByType] AS
                                   SELECT CASE TransactionType
                                   		WHEN 0 THEN 'أيداع'
                                   		WHEN 1 THEN 'سحب'
                                   		WHEN 2 THEN 'تحويل'
                                   		ELSE 'غير معرف'
                                   		END AS TransactionType,
                                   	COUNT(*) AS TotalTransactions,
                                   	(CAST(SUM(Amount) AS varchar) + ' L.S') AS TotalAmount
                                   FROM Transactions
                                   GROUP BY TransactionType;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE View [dbo].[TransactionsSummary] AS
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
                                                                             FROM dbo.Transactions AS T INNER JOIN dbo.Accounts AS A ON T.AccountId = A.AccountId
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE VIEW [dbo].[UserAccounts] AS
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
                                                                            JOIN People C ON CL.PersonId = C.PersonId;
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE VIEW [dbo].[UserDetails] AS
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
                                       Branches B ON U.BranchId = B.BranchId;
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE PROCEDURE [dbo].[SP_AddDebt]
                                       @clientId INT,
                                   	@serviceId INT,
                                   	@quantity SMALLINT,
                                       @description NVARCHAR(100), 
                                       @notes NVARCHAR(100), 
                                       @createdByUserId INT,
                                   	@newDebtId INT OUTPUT
                                   AS
                                   BEGIN
                                   	SET NOCOUNT ON;
                                   
                                       -- Check if the Service and Client and User exists in the Own tables
                                       IF NOT EXISTS 
                                   	(
                                   		SELECT 1 FROM Services S 
                                   		JOIN Clients C ON C.ClientId = @clientId 
                                   		JOIN Users U ON U.UserId = @createdByUserId 
                                   		WHERE S.ServiceId = @serviceId
                                   	)
                                   	BEGIN
                                   		SET @newDebtId = 0; -- Return 0 to indicate (service or client or user) dose not exists
                                   	    RETURN;
                                   	END
                                   
                                   	DECLARE @cost DECIMAL(8, 2),
                                               @discountPercentage DECIMAL(5, 2),
                                               @amountDeducted DECIMAL(8, 2),
                                               @createdAt DATE,
                                   			@status TINYINT,
                                               @servicePrice DECIMAL(8, 2),
                                               @clientType TINYINT;
                                   
                                   
                                   	SELECT @createdAt = GETDATE(),
                                              @status = 0, -- 0 = NotPaid
                                   		   @servicePrice = S.Price, 
                                   		   @clientType = C.ClientType, 
                                   		   @discountPercentage = D.DiscountPercentage
                                   	FROM Services S JOIN Clients C 
                                   	ON C.ClientId = @clientId LEFT JOIN Discounts D 
                                   	ON D.ServiceId = S.ServiceId AND D.ClientType = C.ClientType
                                   	WHERE S.ServiceId = @serviceId;
                                   
                                   
                                   	SET @cost = ROUND(@servicePrice * (1 - COALESCE(@discountPercentage, 0) / 100), -2);
                                       SET @amountDeducted = CASE WHEN @discountPercentage IS NOT NULL THEN (@servicePrice - @cost) * @quantity ELSE NULL END;
                                   
                                       BEGIN TRY
                                   		
                                   		DECLARE @newDebtIdTable TABLE (DebtId INT);
                                   
                                   		INSERT INTO Debts(ClientId, ServiceId, Cost, Quantity, DiscountPercentage, AmountDeducted, Description, Notes, CreatedAt, Status, CreatedByUserId)
                                   		OUTPUT INSERTED.DebtId INTO @newDebtIdTable
                                   		VALUES (@clientId, @serviceId, @cost, @quantity, @discountPercentage, @amountDeducted, @description, @notes, @createdAt, @status, @createdByUserId);
                                   		
                                   		SELECT @newDebtId = DebtId FROM @newDebtIdTable;
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                   		SET @newDebtId = -1; -- Return -1 to indicate an error
                                   
                                       END CATCH
                                   END
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE PROCEDURE [dbo].[SP_AddSale]
                                       @clientId INT,
                                   	@serviceId INT,
                                   	@quantity SMALLINT,
                                       @description NVARCHAR(100), 
                                       @notes NVARCHAR(100), 
                                   	@status TINYINT,
                                       @createdByUserId INT,
                                   	@newSaleId INT OUTPUT
                                   AS
                                   BEGIN
                                   	SET NOCOUNT ON;
                                   
                                       -- Check if the Service and Client and User exists in the Own tables
                                       IF NOT EXISTS 
                                   	(
                                   		SELECT 1 FROM Services S 
                                   		JOIN Clients C ON C.ClientId = @clientId 
                                   		JOIN Users U ON U.UserId = @createdByUserId 
                                   		WHERE S.ServiceId = @serviceId
                                   	)
                                   	BEGIN
                                   		SET @newSaleId = 0; -- Return 0 to indicate (service or client or user) dose not exists
                                   	    RETURN;
                                   	END
                                   
                                   	DECLARE @cost DECIMAL(8, 2),
                                               @discountPercentage DECIMAL(5, 2),
                                               @amountDeducted DECIMAL(8, 2),
                                               @createdAt DATE,
                                               @servicePrice DECIMAL(8, 2),
                                               @clientType TINYINT;
                                   
                                   
                                   	SELECT @createdAt = GETDATE(),
                                   		   @servicePrice = S.Price, 
                                   		   @clientType = C.ClientType, 
                                   		   @discountPercentage = D.DiscountPercentage
                                   	FROM Services S JOIN Clients C 
                                   	ON C.ClientId = @clientId LEFT JOIN Discounts D 
                                   	ON D.ServiceId = S.ServiceId AND D.ClientType = C.ClientType
                                   	WHERE S.ServiceId = @serviceId;
                                   
                                   
                                   	SET @cost = ROUND(@servicePrice * (1 - COALESCE(@discountPercentage, 0) / 100), -2);
                                       SET @amountDeducted = CASE WHEN @discountPercentage IS NOT NULL THEN (@servicePrice - @cost) * @quantity ELSE NULL END;
                                   
                                       BEGIN TRY
                                   		
                                   		DECLARE @newSaleIdTable TABLE (SaleId INT);
                                   
                                   		INSERT INTO Sales (ClientId, ServiceId, Cost, Quantity, DiscountPercentage, AmountDeducted, Description, Notes, CreatedAt, Status, CreatedByUserId)
                                   		OUTPUT INSERTED.SaleId INTO @newSaleIdTable
                                   		VALUES (@clientId, @serviceId, @cost, @quantity, @discountPercentage, @amountDeducted, @description, @notes, @createdAt, @status, @createdByUserId);
                                   		
                                   		SELECT @newSaleId = SaleId FROM @newSaleIdTable;
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                   		SET @newSaleId = -1; -- Return -1 to indicate an error
                                   
                                       END CATCH
                                   END
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE PROCEDURE [dbo].[SP_DepositByAccountID]
                                       @accountId INT, 
                                       @amount DECIMAL(8,2), 
                                       @notes NVARCHAR(100), 
                                       @createdByUserId INT,
                                   	@returnValue INT OUTPUT
                                   AS
                                   BEGIN
                                   	SET NOCOUNT ON;
                                   
                                       -- Check if the Account ID exists in the Accounts table
                                       IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountId = @accountId)
                                   	BEGIN
                                   		SET @returnValue = 1; -- Return 1 if the account does not exist
                                   		RETURN; 
                                   	END
                                   
                                   	BEGIN TRANSACTION;
                                       BEGIN TRY
                                   
                                           -- Update the account balance by adding the deposit amount
                                           UPDATE Accounts 
                                           SET Balance = Balance + @amount
                                           WHERE AccountId = @accountId;
                                   
                                           -- Insert a new transaction record [TransactionType] = {0 = Deposit | 1 = Withdraw}
                                           INSERT INTO Transactions (AccountId, TransactionType, Amount, Notes, CreatedByUserId)
                                           VALUES (@accountId, 0, @amount, @notes, @createdByUserId);
                                   
                                           COMMIT; -- Commit the transaction if everything is successful
                                   
                                   		SET @returnValue = 0; -- Return 0 to indicate success
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                           -- Rollback the transaction if an error occurs
                                           ROLLBACK;
                                   		SET @returnValue = -1; -- Return -1 to indicate an error
                                   
                                       END CATCH
                                   END
                                   
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   CREATE PROCEDURE [dbo].[SP_PayAllDebtsByClientId]
                                       @clientId INT,
                                       @notes NVARCHAR(100),
                                       @createdByUserId INT,
                                   	@returnValue INT OUTPUT
                                   AS
                                   BEGIN
                                       SET NOCOUNT ON;
                                   
                                   	-- Check if there are one Debt at less exists, and is not paid or canceled
                                       IF NOT EXISTS (SELECT Top 1 DebtId FROM Debts WHERE ClientId = @clientId AND Status NOT IN (1, 2))
                                   	BEGIN
                                   		SET @returnValue = 1; -- Debts does not exist or is already paid/canceled
                                   		RETURN;
                                   	END
                                   	
                                   	DECLARE @accountId INT;
                                   
                                   	SELECT @accountId = AccountId FROM Accounts
                                   	WHERE ClientId = @clientId;
                                   
                                   	IF @accountId IS NULL
                                   	BEGIN
                                   		SET @returnValue = 2; -- Account does not exist
                                   		RETURN; 
                                   	END
                                   	
                                       DECLARE @totalAllDebts DECIMAL(8,2);
                                       DECLARE @balance DECIMAL(8,2);
                                   	
                                       SELECT @totalAllDebts = SUM(Total) FROM Debts WHERE ClientId = @clientId AND Status IN (0); -- 0 = Not Paid
                                       SELECT @balance = Balance FROM Accounts WHERE AccountId = @accountId;
                                   	
                                       -- Check if the account has enough balance to pay the debt
                                       IF @totalAllDebts > @balance
                                   	BEGIN
                                   		SET @returnValue = 3; -- Insufficient balance
                                   		RETURN;
                                   	END
                                   	
                                   	BEGIN TRANSACTION;
                                       BEGIN TRY
                                   
                                           -- Update the account balance
                                           UPDATE Accounts 
                                           SET Balance = Balance - @totalAllDebts
                                           WHERE AccountId = @accountId;
                                   
                                           -- Insert a new payment record and capture the PaymentId
                                           DECLARE @paymentId TABLE (PaymentId INT);
                                           INSERT INTO Payments (AccountId, Amount, Notes, CreatedByUserId)
                                           OUTPUT INSERTED.PaymentId INTO @paymentId
                                           VALUES (@accountId, @totalAllDebts, @notes, @createdByUserId);
                                   
                                           -- Update the all debts status to Paid and link the all debts with the payment record
                                           UPDATE Debts
                                           SET Status = 1, -- Paid
                                               PaymentId = (SELECT PaymentId FROM @paymentId)
                                           WHERE ClientId = @clientId AND Status IN (0);
                                   
                                           COMMIT; -- Commit the transaction if everything is successful
                                   		
                                   		SET @returnValue = 0; -- Indicate success
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                           ROLLBACK; -- Rollback the transaction if an error occurs
                                   		SET @returnValue = -1; -- Indicate an error
                                   
                                       END CATCH
                                   
                                   END
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   
                                   
                                   CREATE PROCEDURE [dbo].[SP_PayDebtById]
                                       @debtId INT,
                                       @notes NVARCHAR(100),
                                       @createdByUserId INT,
                                   	@returnValue INT OUTPUT
                                   AS
                                   BEGIN
                                       SET NOCOUNT ON;
                                   
                                   	-- Check if the Debt ID exists, is not paid or canceled
                                           IF NOT EXISTS (SELECT 1 FROM Debts WHERE DebtId = @debtId AND Status NOT IN (1, 2))
                                   		BEGIN
                                   			SET @returnValue = 1; -- Debt does not exist or is already paid/canceled
                                   			RETURN; 
                                   		END
                                   
                                   		DECLARE @accountId INT;
                                   
                                   		SELECT @accountId = A.AccountId FROM Accounts A JOIN Debts D
                                   		ON A.ClientId = D.ClientId
                                   		WHERE D.DebtId = @debtId;
                                   		
                                   		IF @accountId IS NULL
                                   		BEGIN
                                   			SET @returnValue = 2; -- Account does not exist
                                   			RETURN; 
                                   		END
                                   
                                           DECLARE @totalDebt DECIMAL(8,2);
                                           DECLARE @balance DECIMAL(8,2);
                                   
                                           SELECT @totalDebt = Total FROM Debts WHERE DebtId = @debtId;
                                           SELECT @balance = Balance FROM Accounts WHERE AccountId = @accountId;
                                   
                                           -- Check if the account has enough balance to pay the debt
                                           IF @totalDebt > @balance
                                   		BEGIN
                                   			SET @returnValue = 3; -- Insufficient balance
                                   			RETURN;
                                   		END
                                   	
                                   	BEGIN TRANSACTION;
                                       BEGIN TRY
                                   
                                           -- Update the account balance
                                           UPDATE Accounts 
                                           SET Balance = Balance - @totalDebt
                                           WHERE AccountId = @accountId;
                                   
                                           -- Insert a new payment record and capture the PaymentId
                                           DECLARE @PaymentId TABLE (PaymentId INT);
                                           INSERT INTO Payments (AccountId, Amount, Notes, CreatedByUserId)
                                           OUTPUT INSERTED.PaymentId INTO @paymentId
                                           VALUES (@accountId, @totalDebt, @notes, @createdByUserId);
                                   
                                           -- Update the debt status to Paid and link the debt with the payment record
                                           UPDATE Debts
                                           SET Status = 1, -- Paid
                                   			Notes = @notes,
                                               PaymentId = (SELECT PaymentId FROM @paymentId)
                                           WHERE DebtId = @debtId;
                                   
                                           COMMIT; -- Commit the transaction if everything is successful
                                   		
                                   		SET @returnValue = 0;  -- Indicate success
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                           ROLLBACK; -- Rollback the transaction if an error occurs
                                   		SET @returnValue = -1; -- Indicate an error
                                   
                                       END CATCH
                                   
                                   END
                                   GO
                                   
                                   SET ANSI_NULLS ON
                                   GO
                                   SET QUOTED_IDENTIFIER ON
                                   GO
                                   CREATE PROCEDURE [dbo].[SP_WithdrawByAccountID] 
                                       @accountId INT, 
                                       @amount DECIMAL(8,2), 
                                       @notes NVARCHAR(100), 
                                       @createdByUserId INT,
                                   	@returnValue INT OUTPUT
                                   AS
                                   BEGIN
                                   	SET NOCOUNT ON;
                                   
                                       -- Check if the Account ID exists in the Accounts table
                                       IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountId = @accountId)
                                   	BEGIN
                                   		SET @returnValue = 1; -- Return 1 if the account does not exist
                                           RETURN; 
                                   	END
                                   
                                   	-- Check if the [Balance < Amount] in the Accounts table to avoid native balance
                                       IF (SELECT Balance FROM Accounts WHERE AccountId = @accountId) < @amount
                                   	BEGIN
                                   		SET @returnValue = 2; -- Return 2 if the balance less then withdraw amount
                                           RETURN; 
                                   	END
                                   
                                       BEGIN TRANSACTION;
                                       BEGIN TRY
                                   
                                           -- Update the account balance by adding the withdraw amount
                                           UPDATE Accounts 
                                           SET Balance = Balance - @amount
                                           WHERE AccountId = @accountId;
                                   
                                           -- Insert a new transaction record [TransactionType] = {0 = Deposit | 1 = Withdraw}
                                           INSERT INTO Transactions (AccountId, TransactionType, Amount, Notes, CreatedByUserId)
                                           VALUES (@accountId, 1, @amount, @notes, @createdByUserId);
                                   
                                           COMMIT; -- Commit the transaction if everything is successful
                                   
                                   		SET @returnValue = 0; -- Return 0 to indicate success
                                   
                                       END TRY
                                   
                                       BEGIN CATCH
                                   
                                           -- Rollback the transaction if an error occurs
                                           ROLLBACK;
                                   		SET @returnValue = -1; -- Return -1 to indicate an error
                                   
                                       END CATCH
                                   END
                                   GO
                                   ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [dbo].[AccountBalancesTransactions];
                                   DROP VIEW [dbo].[BranchOperationalMetrics];
                                   DROP VIEW [dbo].[ClientDetails];
                                   DROP VIEW [dbo].[ClientsByType];
                                   DROP VIEW [dbo].[ClientsSummary];
                                   DROP VIEW [dbo].[DashboardSummary];
                                   DROP VIEW [dbo].[DebtsByStatus];
                                   DROP VIEW [dbo].[DebtsSummary];
                                   DROP VIEW [dbo].[DiscountsApplied];
                                   DROP VIEW [dbo].[MonthlyFinancialSummary];
                                   DROP VIEW [dbo].[PaymentsSummary];
                                   DROP VIEW [dbo].[PersonDetails];
                                   DROP VIEW [dbo].[RolesSummary];
                                   DROP VIEW [dbo].[SalesSummary];
                                   DROP VIEW [dbo].[ServicesSummary];
                                   DROP VIEW [dbo].[TransactionsByType];
                                   DROP View [dbo].[TransactionsSummary];
                                   DROP VIEW [dbo].[UserAccounts];
                                   DROP VIEW [dbo].[UserDetails];
                                   DROP PROCEDURE [dbo].[SP_AddDebt];
                                   DROP PROCEDURE [dbo].[SP_AddSale];
                                   DROP PROCEDURE [dbo].[SP_DepositByAccountID];
                                   DROP PROCEDURE [dbo].[SP_PayAllDebtsByClientId];
                                   DROP PROCEDURE [dbo].[SP_PayDebtById];
                                   DROP PROCEDURE [dbo].[SP_WithdrawByAccountID];");
        }
    }
}
