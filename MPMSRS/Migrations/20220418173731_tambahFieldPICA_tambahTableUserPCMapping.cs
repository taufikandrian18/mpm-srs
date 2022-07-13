using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class tambahFieldPICA_tambahTableUserPCMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectiveActionProblemIdentification",
                table: "TB_Visiting_Detail_Reports",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_User_Problem_Category_Mappings",
                columns: table => new
                {
                    UserProblemCategoryMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_User_Problem_Category_Mappings", x => x.UserProblemCategoryMappingId);
                    table.ForeignKey(
                        name: "FK_UserProblemCategoryMappings_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProblemCategoryMappings_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Problem_Category_Mappings_EmployeeId",
                table: "TB_User_Problem_Category_Mappings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Problem_Category_Mappings_ProblemCategoryId",
                table: "TB_User_Problem_Category_Mappings",
                column: "ProblemCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_User_Problem_Category_Mappings");

            migrationBuilder.DropColumn(
                name: "CorrectiveActionProblemIdentification",
                table: "TB_Visiting_Detail_Reports");
        }
    }
}
