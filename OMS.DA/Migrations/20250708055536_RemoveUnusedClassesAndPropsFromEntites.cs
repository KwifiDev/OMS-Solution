using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedClassesAndPropsFromEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionsConfig");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "char(44)",
                unicode: false,
                fixedLength: true,
                maxLength: 44,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(64)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 64);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "char(64)",
                unicode: false,
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(44)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 44);

            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PermissionsConfig",
                columns: table => new
                {
                    PermissionConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PermissionNo = table.Column<int>(type: "int", nullable: false, comment: "Bit-Wise Operator Example 1, 2, 4, 8, 16, 32, ...")
                },
                constraints: table =>
                {
                    table.PrimaryKey("permissionsconfig_permissionconfigid_primary", x => x.PermissionConfigId);
                });
        }
    }
}
