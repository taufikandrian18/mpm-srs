using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class pushNotificationRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId",
                table: "TB_PushNotifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TB_PushNotifications_RecipientId",
                table: "TB_PushNotifications",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotificationRecipients_UserRecipients",
                table: "TB_PushNotifications",
                column: "RecipientId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications",
                column: "EmployeeId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushNotificationRecipients_UserRecipients",
                table: "TB_PushNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications");

            migrationBuilder.DropIndex(
                name: "IX_TB_PushNotifications_RecipientId",
                table: "TB_PushNotifications");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "TB_PushNotifications");

            migrationBuilder.AddForeignKey(
                name: "FK_PushNotifications_UserSender",
                table: "TB_PushNotifications",
                column: "EmployeeId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
