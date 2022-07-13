using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class correctiveActionAttachmentsContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Corrective_Action_Attachments_TB_Attachments_AttachmentId",
                table: "TB_Corrective_Action_Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Corrective_Action_Attachments_TB_Corrective_Actions_CorrectiveActionId",
                table: "TB_Corrective_Action_Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Corrective_Action_Attachments",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValueSql: "('SYSTEM')",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_Corrective_Action_Attachments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Corrective_Action_Attachments",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Corrective_Action_Attachments",
                type: "varchar(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValueSql: "('SYSTEM')",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_Corrective_Action_Attachments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActionAttachments_Attachments",
                table: "TB_Corrective_Action_Attachments",
                column: "AttachmentId",
                principalTable: "TB_Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectiveActionAttachments_CorrectiveActions",
                table: "TB_Corrective_Action_Attachments",
                column: "CorrectiveActionId",
                principalTable: "TB_Corrective_Actions",
                principalColumn: "CorrectiveActionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActionAttachments_Attachments",
                table: "TB_Corrective_Action_Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrectiveActionAttachments_CorrectiveActions",
                table: "TB_Corrective_Action_Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Corrective_Action_Attachments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64,
                oldDefaultValueSql: "('SYSTEM')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_Corrective_Action_Attachments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Corrective_Action_Attachments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Corrective_Action_Attachments",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldUnicode: false,
                oldMaxLength: 64,
                oldDefaultValueSql: "('SYSTEM')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_Corrective_Action_Attachments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Corrective_Action_Attachments_TB_Attachments_AttachmentId",
                table: "TB_Corrective_Action_Attachments",
                column: "AttachmentId",
                principalTable: "TB_Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Corrective_Action_Attachments_TB_Corrective_Actions_CorrectiveActionId",
                table: "TB_Corrective_Action_Attachments",
                column: "CorrectiveActionId",
                principalTable: "TB_Corrective_Actions",
                principalColumn: "CorrectiveActionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
