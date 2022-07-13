using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class allowNullsGuidUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users",
                column: "AttachmentId",
                principalTable: "TB_Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AttachmentId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users",
                column: "AttachmentId",
                principalTable: "TB_Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
