using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class userNetwork_Authentications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Authentications",
                columns: table => new
                {
                    AuthenticationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Otp = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    UserData = table.Column<string>(type: "varchar(max)", unicode: false, maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Authentications", x => x.AuthenticationId);
                });

            migrationBuilder.CreateTable(
                name: "TB_User_Network_Mappings",
                columns: table => new
                {
                    UserNetworkMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_User_Network_Mappings", x => x.UserNetworkMappingId);
                    table.ForeignKey(
                        name: "FK_UserNetworkMappings_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNetworkMappings_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Network_Mappings_EmployeeId",
                table: "TB_User_Network_Mappings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Network_Mappings_NetworkId",
                table: "TB_User_Network_Mappings",
                column: "NetworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Authentications");

            migrationBuilder.DropTable(
                name: "TB_User_Network_Mappings");
        }
    }
}
