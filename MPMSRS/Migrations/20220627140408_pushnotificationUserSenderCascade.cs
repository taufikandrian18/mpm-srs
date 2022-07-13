using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class pushnotificationUserSenderCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_Users",
                table: "TB_PushNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications",
                column: "EmployeeId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_Users",
                table: "TB_PushNotifications",
                column: "EmployeeId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
