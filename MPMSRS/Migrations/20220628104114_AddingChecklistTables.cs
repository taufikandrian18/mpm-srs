using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class AddingChecklistTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Checklists",
                columns: table => new
                {
                    ChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChecklistItem = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ChecklistIdentification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ChecklistActualCondition = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ChecklistActualDetail = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: true),
                    ChecklistFix = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: true),
                    ChecklistStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    VisitingDetailReportDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Checklists", x => x.ChecklistId);
                    table.ForeignKey(
                        name: "FK_Checklists_Attachments",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklists_Visitings",
                        column: x => x.VisitingId,
                        principalTable: "TB_Visitings",
                        principalColumn: "VisitingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Checklist_Evidences",
                columns: table => new
                {
                    ChecklistEvidenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Checklist_Evidences", x => x.ChecklistEvidenceId);
                    table.ForeignKey(
                        name: "FK_ChecklistEvidences_Attachments",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistEvidences_Checklists",
                        column: x => x.ChecklistId,
                        principalTable: "TB_Checklists",
                        principalColumn: "ChecklistId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Checklist_PICs",
                columns: table => new
                {
                    ChecklistPICId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Checklist_PICs", x => x.ChecklistPICId);
                    table.ForeignKey(
                        name: "FK_ChecklistPICs_Checklists",
                        column: x => x.ChecklistId,
                        principalTable: "TB_Checklists",
                        principalColumn: "ChecklistId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChecklistPICs_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklist_Evidences_AttachmentId",
                table: "TB_Checklist_Evidences",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklist_Evidences_ChecklistId",
                table: "TB_Checklist_Evidences",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklist_PICs_ChecklistId",
                table: "TB_Checklist_PICs",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklist_PICs_EmployeeId",
                table: "TB_Checklist_PICs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklists_AttachmentId",
                table: "TB_Checklists",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklists_NetworkId",
                table: "TB_Checklists",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Checklists_VisitingId",
                table: "TB_Checklists",
                column: "VisitingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Checklist_Evidences");

            migrationBuilder.DropTable(
                name: "TB_Checklist_PICs");

            migrationBuilder.DropTable(
                name: "TB_Checklists");
        }
    }
}
