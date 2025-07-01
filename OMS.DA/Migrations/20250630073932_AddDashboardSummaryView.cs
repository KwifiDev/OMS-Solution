using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardSummaryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW DashboardSummary AS
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
                                              (s.TotalSales + d.PaidDebts) AS TotalIncome,
                                              (d.NotPaidDebts) AS OutstandingDebts
                                          FROM 
                                              RevenueData r
                                              CROSS JOIN TransactionData t
                                              CROSS JOIN AccountData a
                                              CROSS JOIN PaymentData p
                                              CROSS JOIN SalesData s
                                              CROSS JOIN DebtData d;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW DashboardSummary");
        }
    }
}
