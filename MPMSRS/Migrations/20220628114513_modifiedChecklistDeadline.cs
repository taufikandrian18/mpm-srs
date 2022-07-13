using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class modifiedChecklistDeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisitingDetailReportDeadline",
                table: "TB_Checklists",
                newName: "ChecklistDeadline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChecklistDeadline",
                table: "TB_Checklists",
                newName: "VisitingDetailReportDeadline");
        }
    }
}
