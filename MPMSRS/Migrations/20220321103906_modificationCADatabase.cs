using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class modificationCADatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_ProblemCategories",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_Visitings",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropIndex(
                name: "IX_TB_Corrective_Actions_ProblemCategoryId",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionDeadline",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionStatus",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropColumn(
                name: "ProblemCategoryId",
                table: "TB_Corrective_Actions");

            migrationBuilder.RenameColumn(
                name: "VisitingId",
                table: "TB_Corrective_Actions",
                newName: "VisitingDetailReportId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Corrective_Actions_VisitingId",
                table: "TB_Corrective_Actions",
                newName: "IX_TB_Corrective_Actions_VisitingDetailReportId");

            migrationBuilder.AddColumn<bool>(
                name: "VisitingDetailReportStatus",
                table: "TB_Visiting_Detail_Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveActionDetail",
                table: "TB_Corrective_Actions",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectiveActionName",
                table: "TB_Corrective_Actions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_VisitingDetailReports",
                table: "TB_Corrective_Actions",
                column: "VisitingDetailReportId",
                principalTable: "TB_Visiting_Detail_Reports",
                principalColumn: "VisitingDetailReportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_VisitingDetailReports",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropColumn(
                name: "VisitingDetailReportStatus",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionDetail",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionName",
                table: "TB_Corrective_Actions");

            migrationBuilder.RenameColumn(
                name: "VisitingDetailReportId",
                table: "TB_Corrective_Actions",
                newName: "VisitingId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Corrective_Actions_VisitingDetailReportId",
                table: "TB_Corrective_Actions",
                newName: "IX_TB_Corrective_Actions_VisitingId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CorrectiveActionDeadline",
                table: "TB_Corrective_Actions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<bool>(
                name: "CorrectiveActionStatus",
                table: "TB_Corrective_Actions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProblemCategoryId",
                table: "TB_Corrective_Actions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Actions_ProblemCategoryId",
                table: "TB_Corrective_Actions",
                column: "ProblemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_ProblemCategories",
                table: "TB_Corrective_Actions",
                column: "ProblemCategoryId",
                principalTable: "TB_Problem_Categories",
                principalColumn: "ProblemCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_Visitings",
                table: "TB_Corrective_Actions",
                column: "VisitingId",
                principalTable: "TB_Visitings",
                principalColumn: "VisitingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
