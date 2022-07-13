using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class divisionNetworkAddToVisitingDetailReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_VisitingDetails",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitingDetailReports_VisitingDetails",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Details");

            migrationBuilder.RenameColumn(
                name: "VisitingDetailId",
                table: "TB_Visiting_Detail_Reports",
                newName: "VisitingId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Visiting_Detail_Reports_VisitingDetailId",
                table: "TB_Visiting_Detail_Reports",
                newName: "IX_TB_Visiting_Detail_Reports_VisitingId");

            migrationBuilder.RenameColumn(
                name: "VisitingDetailId",
                table: "TB_Corrective_Actions",
                newName: "VisitingId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Corrective_Actions_VisitingDetailId",
                table: "TB_Corrective_Actions",
                newName: "IX_TB_Corrective_Actions_VisitingId");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByGM",
                table: "TB_Visitings",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByManager",
                table: "TB_Visitings",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitingComment",
                table: "TB_Visitings",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "VisitingStatus",
                table: "TB_Visitings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DivisionId",
                table: "TB_Visiting_Detail_Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkId",
                table: "TB_Visiting_Detail_Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Note_Mappings",
                columns: table => new
                {
                    VisitingNoteMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingNoteMappingDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Note_Mappings", x => x.VisitingNoteMappingId);
                    table.ForeignKey(
                        name: "FK_VisitingNoteMappings_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingNoteMappings_Visitings",
                        column: x => x.VisitingId,
                        principalTable: "TB_Visitings",
                        principalColumn: "VisitingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Reports_DivisionId",
                table: "TB_Visiting_Detail_Reports",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Reports_NetworkId",
                table: "TB_Visiting_Detail_Reports",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Note_Mappings_EmployeeId",
                table: "TB_Visiting_Note_Mappings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Note_Mappings_VisitingId",
                table: "TB_Visiting_Note_Mappings",
                column: "VisitingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_Visitings",
                table: "TB_Corrective_Actions",
                column: "VisitingId",
                principalTable: "TB_Visitings",
                principalColumn: "VisitingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingDetailReports_Divisions",
                table: "TB_Visiting_Detail_Reports",
                column: "DivisionId",
                principalTable: "TB_Divisions",
                principalColumn: "DivisionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingDetailReports_Networks",
                table: "TB_Visiting_Detail_Reports",
                column: "NetworkId",
                principalTable: "TB_Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingDetailReports_Visitings",
                table: "TB_Visiting_Detail_Reports",
                column: "VisitingId",
                principalTable: "TB_Visitings",
                principalColumn: "VisitingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActions_Visitings",
                table: "TB_Corrective_Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitingDetailReports_Divisions",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitingDetailReports_Networks",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitingDetailReports_Visitings",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Note_Mappings");

            migrationBuilder.DropIndex(
                name: "IX_TB_Visiting_Detail_Reports_DivisionId",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropIndex(
                name: "IX_TB_Visiting_Detail_Reports_NetworkId",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropColumn(
                name: "ApprovedByGM",
                table: "TB_Visitings");

            migrationBuilder.DropColumn(
                name: "ApprovedByManager",
                table: "TB_Visitings");

            migrationBuilder.DropColumn(
                name: "VisitingComment",
                table: "TB_Visitings");

            migrationBuilder.DropColumn(
                name: "VisitingStatus",
                table: "TB_Visitings");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "TB_Visiting_Detail_Reports");

            migrationBuilder.RenameColumn(
                name: "VisitingId",
                table: "TB_Visiting_Detail_Reports",
                newName: "VisitingDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Visiting_Detail_Reports_VisitingId",
                table: "TB_Visiting_Detail_Reports",
                newName: "IX_TB_Visiting_Detail_Reports_VisitingDetailId");

            migrationBuilder.RenameColumn(
                name: "VisitingId",
                table: "TB_Corrective_Actions",
                newName: "VisitingDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Corrective_Actions_VisitingId",
                table: "TB_Corrective_Actions",
                newName: "IX_TB_Corrective_Actions_VisitingDetailId");

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Details",
                columns: table => new
                {
                    VisitingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedByGM = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ApprovedByManager = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    VisitingDetailComment = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VisitingDetailNotes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VisitingDetailStatus = table.Column<bool>(type: "bit", nullable: false),
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Details", x => x.VisitingDetailId);
                    table.ForeignKey(
                        name: "FK_VisitingDetails_Visitings",
                        column: x => x.VisitingId,
                        principalTable: "TB_Visitings",
                        principalColumn: "VisitingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Details_VisitingId",
                table: "TB_Visiting_Details",
                column: "VisitingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActions_VisitingDetails",
                table: "TB_Corrective_Actions",
                column: "VisitingDetailId",
                principalTable: "TB_Visiting_Details",
                principalColumn: "VisitingDetailId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitingDetailReports_VisitingDetails",
                table: "TB_Visiting_Detail_Reports",
                column: "VisitingDetailId",
                principalTable: "TB_Visiting_Details",
                principalColumn: "VisitingDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
