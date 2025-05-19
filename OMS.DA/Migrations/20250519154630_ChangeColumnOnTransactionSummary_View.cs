using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnOnTransactionSummary_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Alter View TransactionsSummary AS
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Alter View TransactionsSummary AS
                                   SELECT T.TransactionId,
                                          A.UserAccount,
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
        }
    }
}
