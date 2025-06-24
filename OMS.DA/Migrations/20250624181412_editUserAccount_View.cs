using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class editUserAccount_View : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW UserAccounts AS
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW UserAccounts AS
                                         SELECT A.AccountId,
                                         	    A.UserAccount,
                                         	    ClientName = (C.FirstName + ' ' + C.LastName),
                                         	   u CASE CL.ClientType
                                         	    WHEN 0 THEN 'عادي'
                                         	    WHEN 1 THEN 'محامي'
                                         	    WHEN 2 THEN 'غير ذلك'
                                         	    ELSE 'غير معرف'
                                         	    END AS ClientType,
                                         	    (CAST(A.Balance AS varchar(15)) + ' L.S') AS ClientBalance
                                         FROM Accounts A
                                         JOIN Clients CL ON A.ClientId = CL.ClientId
                                         JOIN People C ON CL.PersonId = C.PersonId;");
        }
    }
}
