using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class removeProblemCategoryFromVisitingDetailReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitingDetailReports_ProblemCategories",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropIndex(
                name: "IX_TB_Visiting_Detail_Reports_ProblemCategoryId",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropColumn(
                name: "ProblemCategoryId",
                table: "TB_Visiting_Detail_Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProblemCategoryId",
                table: "TB_Visiting_Detail_Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Reports_ProblemCategoryId",
                table: "TB_Visiting_Detail_Reports",
                column: "ProblemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingDetailReports_ProblemCategories",
                table: "TB_Visiting_Detail_Reports",
                column: "ProblemCategoryId",
                principalTable: "TB_Problem_Categories",
                principalColumn: "ProblemCategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
