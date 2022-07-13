using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class tambahdatajaringandanjabatan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Networks",
                columns: table => new
                {
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AhmCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MDCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ChannelDealer = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DLREmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KodeKareswil = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Karesidenan = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    NPKSupervisor = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    NamaSupervisor = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: true),
                    IdKaresidenanHC3 = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NamaKaresHC3 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NPKSpvHC3 = table.Column<int>(type: "int", nullable: false),
                    NamaSPVHC3 = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    NPKDeptHeadHC3 = table.Column<int>(type: "int", nullable: false),
                    NamaDeptHeadHC3 = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    NPKDivHeadHC3 = table.Column<int>(type: "int", nullable: false),
                    NamaDivHeadHC3 = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    IdKaresidenanTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NamaKaresTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NPKSpvTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NamaSPVTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NPKDeptHeadTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NamaDeptHeadTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NPKDivHeadTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NamaDivHeadTSD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Networks", x => x.NetworkId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Positions",
                columns: table => new
                {
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    KodeJabatan = table.Column<int>(type: "int", nullable: false),
                    NamaJabatan = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    GroupJabatanId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    NamaGroupPosition = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Positions", x => x.PositionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Networks");

            migrationBuilder.DropTable(
                name: "TB_Positions");
        }
    }
}
