using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class changeEventwithEventMasterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDetailReports_Networks",
                table: "TB_Event_Detail_Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Networks",
                table: "TB_Events");

            migrationBuilder.RenameColumn(
                name: "NetworkId",
                table: "TB_Events",
                newName: "EventMasterDataId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Events_NetworkId",
                table: "TB_Events",
                newName: "IX_TB_Events_EventMasterDataId");

            migrationBuilder.RenameColumn(
                name: "NetworkId",
                table: "TB_Event_Detail_Reports",
                newName: "EventMasterDataId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Event_Detail_Reports_NetworkId",
                table: "TB_Event_Detail_Reports",
                newName: "IX_TB_Event_Detail_Reports_EventMasterDataId");

            migrationBuilder.CreateTable(
                name: "TB_Event_Master_Datas",
                columns: table => new
                {
                    EventMasterDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventMasterDataName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    EventMasterDataLocation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    EventMasterDataLatitude = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventMasterDataLongitude = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_TB_Event_Master_Datas", x => x.EventMasterDataId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventDetailReports_EventMasterDatas",
                table: "TB_Event_Detail_Reports",
                column: "EventMasterDataId",
                principalTable: "TB_Event_Master_Datas",
                principalColumn: "EventMasterDataId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventMasterDatas",
                table: "TB_Events",
                column: "EventMasterDataId",
                principalTable: "TB_Event_Master_Datas",
                principalColumn: "EventMasterDataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDetailReports_EventMasterDatas",
                table: "TB_Event_Detail_Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventMasterDatas",
                table: "TB_Events");

            migrationBuilder.DropTable(
                name: "TB_Event_Master_Datas");

            migrationBuilder.RenameColumn(
                name: "EventMasterDataId",
                table: "TB_Events",
                newName: "NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Events_EventMasterDataId",
                table: "TB_Events",
                newName: "IX_TB_Events_NetworkId");

            migrationBuilder.RenameColumn(
                name: "EventMasterDataId",
                table: "TB_Event_Detail_Reports",
                newName: "NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Event_Detail_Reports_EventMasterDataId",
                table: "TB_Event_Detail_Reports",
                newName: "IX_TB_Event_Detail_Reports_NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDetailReports_Networks",
                table: "TB_Event_Detail_Reports",
                column: "NetworkId",
                principalTable: "TB_Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Networks",
                table: "TB_Events",
                column: "NetworkId",
                principalTable: "TB_Networks",
                principalColumn: "NetworkId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
