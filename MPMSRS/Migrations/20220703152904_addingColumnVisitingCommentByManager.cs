using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class addingColumnVisitingCommentByManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VisitingCommentByManager",
                table: "TB_Visitings",
                type: "varchar(max)",
                unicode: false,
                maxLength: 2147483647,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitingCommentByManager",
                table: "TB_Visitings");
        }
    }
}
