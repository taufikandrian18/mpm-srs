using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class NetworkLongLat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetworkLogitude",
                table: "TB_Networks");

            migrationBuilder.AlterColumn<string>(
                name: "NetworkLatitude",
                table: "TB_Networks",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetworkLongitude",
                table: "TB_Networks",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetworkLongitude",
                table: "TB_Networks");

            migrationBuilder.AlterColumn<string>(
                name: "NetworkLatitude",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetworkLogitude",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
