using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class AlterPersonDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW PersonDetails AS
                                   SELECT PersonId,
                                   	      FullName = (FirstName + ' ' + LastName),
                                   	      ISNULL(Phone, 'لا يوجد') AS Phone,
                                          Gender
                                   FROM People;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER VIEW PersonDetails AS
                                   SELECT PersonId,
                                   	      FullName = (FirstName + ' ' + LastName),
                                   	      ISNULL(Phone, 'لا يوجد') AS Phone
                                   FROM People;");
        }
    }
}
