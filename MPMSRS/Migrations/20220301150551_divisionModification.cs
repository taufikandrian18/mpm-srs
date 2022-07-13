using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class divisionModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Division",
                table: "TB_Users");

            migrationBuilder.AddColumn<Guid>(
                name: "DivisionId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Users_DivisionId",
                table: "TB_Users",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Divisions",
                table: "TB_Users",
                column: "DivisionId",
                principalTable: "TB_Divisions",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Divisions",
                table: "TB_Users");

            migrationBuilder.DropIndex(
                name: "IX_TB_Users_DivisionId",
                table: "TB_Users");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "TB_Users");

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true);
        }
    }
}
