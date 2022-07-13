using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class EventFeatureDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NetworkLatitude",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetworkLogitude",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventComment = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EventStatus = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ApprovedByManager = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ApprovedByGM = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Detail_Reports",
                columns: table => new
                {
                    EventDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportProblemIdentification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventCAProblemIdentification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventDetailReportStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EventDetailReportDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventDetailReportFlagging = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Detail_Reports", x => x.EventDetailReportId);
                    table.ForeignKey(
                        name: "FK_EventDetailReports_Divisions",
                        column: x => x.DivisionId,
                        principalTable: "TB_Divisions",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDetailReports_Events",
                        column: x => x.EventId,
                        principalTable: "TB_Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDetailReports_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Peoples",
                columns: table => new
                {
                    EventPeopleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Peoples", x => x.EventPeopleId);
                    table.ForeignKey(
                        name: "FK_EventPeoples_Events",
                        column: x => x.EventId,
                        principalTable: "TB_Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventPeoples_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Corrective_Actions",
                columns: table => new
                {
                    EventCAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCAName = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ProgressBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValidateBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EventCADetail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Corrective_Actions", x => x.EventCAId);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActions_EventDetailReports",
                        column: x => x.EventDetailReportId,
                        principalTable: "TB_Event_Detail_Reports",
                        principalColumn: "EventDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Detail_Report_Attachments",
                columns: table => new
                {
                    EventDetailReportAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Detail_Report_Attachments", x => x.EventDetailReportAttachmentId);
                    table.ForeignKey(
                        name: "FK_EventDetailReportAttachments_Attachments",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDetailReportAttachments_EventDetailReports",
                        column: x => x.EventDetailReportId,
                        principalTable: "TB_Event_Detail_Reports",
                        principalColumn: "EventDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Detail_Report_PICs",
                columns: table => new
                {
                    EventDetailReportPICId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Detail_Report_PICs", x => x.EventDetailReportPICId);
                    table.ForeignKey(
                        name: "FK_EventDetailReportPICs_EventDetailReports",
                        column: x => x.EventDetailReportId,
                        principalTable: "TB_Event_Detail_Reports",
                        principalColumn: "EventDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDetailReportPICs_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Detail_Report_Problem_Categories",
                columns: table => new
                {
                    EventDetailReportPCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetailReportPCName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Detail_Report_Problem_Categories", x => x.EventDetailReportPCId);
                    table.ForeignKey(
                        name: "FK_EventDetailReportProblemCategories_EventDetailReports",
                        column: x => x.EventDetailReportId,
                        principalTable: "TB_Event_Detail_Reports",
                        principalColumn: "EventDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventDetailReportProblemCategories_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Corrective_Action_Attachments",
                columns: table => new
                {
                    EventCAAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Corrective_Action_Attachments", x => x.EventCAAttachmentId);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionAttachments_Attachments",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionAttachments_EventCorrectiveActions",
                        column: x => x.EventCAId,
                        principalTable: "TB_Event_Corrective_Actions",
                        principalColumn: "EventCAId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Corrective_Action_PICs",
                columns: table => new
                {
                    EventCAPICId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Corrective_Action_PICs", x => x.EventCAPICId);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionPICs_EventCorrectiveActions",
                        column: x => x.EventCAId,
                        principalTable: "TB_Event_Corrective_Actions",
                        principalColumn: "EventCAId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionPICs_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Event_Corrective_Action_Problem_Categories",
                columns: table => new
                {
                    EventCAPCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventCAPCName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Event_Corrective_Action_Problem_Categories", x => x.EventCAPCId);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionProblemCategories_EventCorrectiveActions",
                        column: x => x.EventCAId,
                        principalTable: "TB_Event_Corrective_Actions",
                        principalColumn: "EventCAId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventCorrectiveActionProblemCategories_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_Attachments_AttachmentId",
                table: "TB_Event_Corrective_Action_Attachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_Attachments_EventCAId",
                table: "TB_Event_Corrective_Action_Attachments",
                column: "EventCAId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_PICs_EmployeeId",
                table: "TB_Event_Corrective_Action_PICs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_PICs_EventCAId",
                table: "TB_Event_Corrective_Action_PICs",
                column: "EventCAId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_Problem_Categories_EventCAId",
                table: "TB_Event_Corrective_Action_Problem_Categories",
                column: "EventCAId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Action_Problem_Categories_ProblemCategoryId",
                table: "TB_Event_Corrective_Action_Problem_Categories",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Corrective_Actions_EventDetailReportId",
                table: "TB_Event_Corrective_Actions",
                column: "EventDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_Attachments_AttachmentId",
                table: "TB_Event_Detail_Report_Attachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_Attachments_EventDetailReportId",
                table: "TB_Event_Detail_Report_Attachments",
                column: "EventDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_PICs_EmployeeId",
                table: "TB_Event_Detail_Report_PICs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_PICs_EventDetailReportId",
                table: "TB_Event_Detail_Report_PICs",
                column: "EventDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_Problem_Categories_EventDetailReportId",
                table: "TB_Event_Detail_Report_Problem_Categories",
                column: "EventDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Report_Problem_Categories_ProblemCategoryId",
                table: "TB_Event_Detail_Report_Problem_Categories",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Reports_DivisionId",
                table: "TB_Event_Detail_Reports",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Reports_EventId",
                table: "TB_Event_Detail_Reports",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Detail_Reports_NetworkId",
                table: "TB_Event_Detail_Reports",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Peoples_EmployeeId",
                table: "TB_Event_Peoples",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Event_Peoples_EventId",
                table: "TB_Event_Peoples",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Events_NetworkId",
                table: "TB_Events",
                column: "NetworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Event_Corrective_Action_Attachments");

            migrationBuilder.DropTable(
                name: "TB_Event_Corrective_Action_PICs");

            migrationBuilder.DropTable(
                name: "TB_Event_Corrective_Action_Problem_Categories");

            migrationBuilder.DropTable(
                name: "TB_Event_Detail_Report_Attachments");

            migrationBuilder.DropTable(
                name: "TB_Event_Detail_Report_PICs");

            migrationBuilder.DropTable(
                name: "TB_Event_Detail_Report_Problem_Categories");

            migrationBuilder.DropTable(
                name: "TB_Event_Peoples");

            migrationBuilder.DropTable(
                name: "TB_Event_Corrective_Actions");

            migrationBuilder.DropTable(
                name: "TB_Event_Detail_Reports");

            migrationBuilder.DropTable(
                name: "TB_Events");

            migrationBuilder.DropColumn(
                name: "NetworkLatitude",
                table: "TB_Networks");

            migrationBuilder.DropColumn(
                name: "NetworkLogitude",
                table: "TB_Networks");
        }
    }
}
