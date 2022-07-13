using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class addVisitingTypeIdEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VisitingTypeId",
                table: "TB_Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TB_Events_VisitingTypeId",
                table: "TB_Events",
                column: "VisitingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_VisitingTypes",
                table: "TB_Events",
                column: "VisitingTypeId",
                principalTable: "TB_Visiting_Types",
                principalColumn: "VisitingTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_VisitingTypes",
                table: "TB_Events");

            migrationBuilder.DropIndex(
                name: "IX_TB_Events_VisitingTypeId",
                table: "TB_Events");

            migrationBuilder.DropColumn(
                name: "VisitingTypeId",
                table: "TB_Events");
        }
    }
}
