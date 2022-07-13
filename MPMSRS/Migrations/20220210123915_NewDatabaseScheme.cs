using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPMSRS.Migrations
{
    public partial class NewDatabaseScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_Users");

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocation",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Users",
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
                table: "TB_Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Users",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "InternalTitle",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Division",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Users",
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
                table: "TB_Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "TB_Users",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "TB_Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Positions",
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
                table: "TB_Positions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "NamaJabatan",
                table: "TB_Positions",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NamaGroupPosition",
                table: "TB_Positions",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Positions",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "GroupJabatanId",
                table: "TB_Positions",
                type: "varchar(11)",
                unicode: false,
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Positions",
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
                table: "TB_Positions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Channel",
                table: "TB_Positions",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Networks",
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
                table: "TB_Networks",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "NamaSPVTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaSPVHC3",
                table: "TB_Networks",
                type: "varchar(23)",
                unicode: false,
                maxLength: 23,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(23)",
                oldMaxLength: 23,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaKaresTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaKaresHC3",
                table: "TB_Networks",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDivHeadTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDivHeadHC3",
                table: "TB_Networks",
                type: "varchar(24)",
                unicode: false,
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDeptHeadTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDeptHeadHC3",
                table: "TB_Networks",
                type: "varchar(17)",
                unicode: false,
                maxLength: 17,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKSupervisor",
                table: "TB_Networks",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKSpvTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKDivHeadTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKDeptHeadTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MDCode",
                table: "TB_Networks",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "KodeKareswil",
                table: "TB_Networks",
                type: "varchar(4)",
                unicode: false,
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Karesidenan",
                table: "TB_Networks",
                type: "varchar(11)",
                unicode: false,
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Networks",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IdKaresidenanTSD",
                table: "TB_Networks",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdKaresidenanHC3",
                table: "TB_Networks",
                type: "varchar(14)",
                unicode: false,
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TB_Networks",
                type: "varchar(33)",
                unicode: false,
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(33)",
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DealerName",
                table: "TB_Networks",
                type: "varchar(36)",
                unicode: false,
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "DLREmail",
                table: "TB_Networks",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Networks",
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
                table: "TB_Networks",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "TB_Networks",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelDealer",
                table: "TB_Networks",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "AhmCode",
                table: "TB_Networks",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TB_Networks",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNum",
                table: "TB_Networks",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "varchar(max)",
                unicode: false,
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "TB_Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    AttachmentUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AttachmentMime = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Attachments", x => x.AttachmentId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Divisions",
                columns: table => new
                {
                    DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DivisionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Divisions", x => x.DivisionId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Problem_Categories",
                columns: table => new
                {
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChildId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Problem_Categories", x => x.ProblemCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Types",
                columns: table => new
                {
                    VisitingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingTypeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Types", x => x.VisitingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Divisions",
                        column: x => x.DivisionId,
                        principalTable: "TB_Divisions",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignments_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visitings",
                columns: table => new
                {
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_TB_Visitings", x => x.VisitingId);
                    table.ForeignKey(
                        name: "FK_Visitings_Networks",
                        column: x => x.NetworkId,
                        principalTable: "TB_Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitings_VisitingTypes",
                        column: x => x.VisitingTypeId,
                        principalTable: "TB_Visiting_Types",
                        principalColumn: "VisitingTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Details",
                columns: table => new
                {
                    VisitingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailNotes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VisitingDetailComment = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VisitingDetailStatus = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByManager = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ApprovedByGM = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
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

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Peoples",
                columns: table => new
                {
                    VisitingPeopleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Peoples", x => x.VisitingPeopleId);
                    table.ForeignKey(
                        name: "FK_VisitingPeoples_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingPeoples_Visitings",
                        column: x => x.VisitingId,
                        principalTable: "TB_Visitings",
                        principalColumn: "VisitingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Corrective_Actions",
                columns: table => new
                {
                    CorrectiveActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectiveActionDeadline = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ProgressBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValidateBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CorrectiveActionStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Corrective_Actions", x => x.CorrectiveActionId);
                    table.ForeignKey(
                        name: "FK_CorrectiveActions_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectiveActions_VisitingDetails",
                        column: x => x.VisitingDetailId,
                        principalTable: "TB_Visiting_Details",
                        principalColumn: "VisitingDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Detail_Reports",
                columns: table => new
                {
                    VisitingDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailReportDeadline = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Detail_Reports", x => x.VisitingDetailReportId);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReports_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReports_VisitingDetails",
                        column: x => x.VisitingDetailId,
                        principalTable: "TB_Visiting_Details",
                        principalColumn: "VisitingDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Corrective_Action_Attachments",
                columns: table => new
                {
                    CorrectiveActionAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectiveActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Corrective_Action_Attachments", x => x.CorrectiveActionAttachmentId);
                    table.ForeignKey(
                        name: "FK_TB_Corrective_Action_Attachments_TB_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Corrective_Action_Attachments_TB_Corrective_Actions_CorrectiveActionId",
                        column: x => x.CorrectiveActionId,
                        principalTable: "TB_Corrective_Actions",
                        principalColumn: "CorrectiveActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Corrective_Action_PICs",
                columns: table => new
                {
                    CorrectiveActionPICId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectiveActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Corrective_Action_PICs", x => x.CorrectiveActionPICId);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionPICs_CorrectiveActions",
                        column: x => x.CorrectiveActionId,
                        principalTable: "TB_Corrective_Actions",
                        principalColumn: "CorrectiveActionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionPICs_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Corrective_Action_Problem_Categories",
                columns: table => new
                {
                    CorrectiveActionPCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectiveActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectiveActionPCName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Corrective_Action_Problem_Categories", x => x.CorrectiveActionPCId);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionProblemCategories_CorrectiveActions",
                        column: x => x.CorrectiveActionId,
                        principalTable: "TB_Corrective_Actions",
                        principalColumn: "CorrectiveActionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrectiveActionProblemCategories_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Detail_Report_Attachments",
                columns: table => new
                {
                    VisitingDetailReportAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Detail_Report_Attachments", x => x.VisitingDetailReportAttachmentId);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportAttachments_Attachments",
                        column: x => x.AttachmentId,
                        principalTable: "TB_Attachments",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportAttachments_VisitingDetailReports",
                        column: x => x.VisitingDetailReportId,
                        principalTable: "TB_Visiting_Detail_Reports",
                        principalColumn: "VisitingDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Detail_Report_PICs",
                columns: table => new
                {
                    VisitingDetailReportPICId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Detail_Report_PICs", x => x.VisitingDetailReportPICId);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportPICs_Users",
                        column: x => x.EmployeeId,
                        principalTable: "TB_Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportPICs_VisitingDetailReports",
                        column: x => x.VisitingDetailReportId,
                        principalTable: "TB_Visiting_Detail_Reports",
                        principalColumn: "VisitingDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Visiting_Detail_Report_Problem_Categories",
                columns: table => new
                {
                    VisitingDetailReportPCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitingDetailReportPCName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, defaultValueSql: "('SYSTEM')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Visiting_Detail_Report_Problem_Categories", x => x.VisitingDetailReportPCId);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportProblemCategories_ProblemCategories",
                        column: x => x.ProblemCategoryId,
                        principalTable: "TB_Problem_Categories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitingDetailReportProblemCategories_VisitingDetailReports",
                        column: x => x.VisitingDetailReportId,
                        principalTable: "TB_Visiting_Detail_Reports",
                        principalColumn: "VisitingDetailReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Users_AttachmentId",
                table: "TB_Users",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Users_RoleId",
                table: "TB_Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_EmployeeId",
                table: "RefreshTokens",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Assignments_DivisionId",
                table: "TB_Assignments",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Assignments_EmployeeId",
                table: "TB_Assignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Assignments_NetworkId",
                table: "TB_Assignments",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_Attachments_AttachmentId",
                table: "TB_Corrective_Action_Attachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_Attachments_CorrectiveActionId",
                table: "TB_Corrective_Action_Attachments",
                column: "CorrectiveActionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_PICs_CorrectiveActionId",
                table: "TB_Corrective_Action_PICs",
                column: "CorrectiveActionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_PICs_EmployeeId",
                table: "TB_Corrective_Action_PICs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_Problem_Categories_CorrectiveActionId",
                table: "TB_Corrective_Action_Problem_Categories",
                column: "CorrectiveActionId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Action_Problem_Categories_ProblemCategoryId",
                table: "TB_Corrective_Action_Problem_Categories",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Actions_ProblemCategoryId",
                table: "TB_Corrective_Actions",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Corrective_Actions_VisitingDetailId",
                table: "TB_Corrective_Actions",
                column: "VisitingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_Attachments_AttachmentId",
                table: "TB_Visiting_Detail_Report_Attachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_Attachments_VisitingDetailReportId",
                table: "TB_Visiting_Detail_Report_Attachments",
                column: "VisitingDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_PICs_EmployeeId",
                table: "TB_Visiting_Detail_Report_PICs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_PICs_VisitingDetailReportId",
                table: "TB_Visiting_Detail_Report_PICs",
                column: "VisitingDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_Problem_Categories_ProblemCategoryId",
                table: "TB_Visiting_Detail_Report_Problem_Categories",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Report_Problem_Categories_VisitingDetailReportId",
                table: "TB_Visiting_Detail_Report_Problem_Categories",
                column: "VisitingDetailReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Reports_ProblemCategoryId",
                table: "TB_Visiting_Detail_Reports",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Detail_Reports_VisitingDetailId",
                table: "TB_Visiting_Detail_Reports",
                column: "VisitingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Details_VisitingId",
                table: "TB_Visiting_Details",
                column: "VisitingId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Peoples_EmployeeId",
                table: "TB_Visiting_Peoples",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visiting_Peoples_VisitingId",
                table: "TB_Visiting_Peoples",
                column: "VisitingId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visitings_NetworkId",
                table: "TB_Visitings",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Visitings_VisitingTypeId",
                table: "TB_Visitings",
                column: "VisitingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users",
                table: "RefreshTokens",
                column: "EmployeeId",
                principalTable: "TB_Users",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users",
                column: "AttachmentId",
                principalTable: "TB_Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles",
                table: "TB_Users",
                column: "RoleId",
                principalTable: "TB_Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments",
                table: "TB_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles",
                table: "TB_Users");

            migrationBuilder.DropTable(
                name: "TB_Assignments");

            migrationBuilder.DropTable(
                name: "TB_Corrective_Action_Attachments");

            migrationBuilder.DropTable(
                name: "TB_Corrective_Action_PICs");

            migrationBuilder.DropTable(
                name: "TB_Corrective_Action_Problem_Categories");

            migrationBuilder.DropTable(
                name: "TB_Roles");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Detail_Report_Attachments");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Detail_Report_PICs");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Detail_Report_Problem_Categories");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Peoples");

            migrationBuilder.DropTable(
                name: "TB_Divisions");

            migrationBuilder.DropTable(
                name: "TB_Corrective_Actions");

            migrationBuilder.DropTable(
                name: "TB_Attachments");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Detail_Reports");

            migrationBuilder.DropTable(
                name: "TB_Problem_Categories");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Details");

            migrationBuilder.DropTable(
                name: "TB_Visitings");

            migrationBuilder.DropTable(
                name: "TB_Visiting_Types");

            migrationBuilder.DropIndex(
                name: "IX_TB_Users_AttachmentId",
                table: "TB_Users");

            migrationBuilder.DropIndex(
                name: "IX_TB_Users_RoleId",
                table: "TB_Users");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_EmployeeId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "TB_Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "TB_Users");

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocation",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Users",
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
                table: "TB_Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "InternalTitle",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Division",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Users",
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
                table: "TB_Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Positions",
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
                table: "TB_Positions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "NamaJabatan",
                table: "TB_Positions",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NamaGroupPosition",
                table: "TB_Positions",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Positions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "GroupJabatanId",
                table: "TB_Positions",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldUnicode: false,
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Positions",
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
                table: "TB_Positions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Channel",
                table: "TB_Positions",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldUnicode: false,
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TB_Networks",
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
                table: "TB_Networks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "NamaSPVTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaSPVHC3",
                table: "TB_Networks",
                type: "nvarchar(23)",
                maxLength: 23,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(23)",
                oldUnicode: false,
                oldMaxLength: 23,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaKaresTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaKaresHC3",
                table: "TB_Networks",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDivHeadTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDivHeadHC3",
                table: "TB_Networks",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldUnicode: false,
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDeptHeadTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NamaDeptHeadHC3",
                table: "TB_Networks",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(17)",
                oldUnicode: false,
                oldMaxLength: 17,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKSupervisor",
                table: "TB_Networks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKSpvTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKDivHeadTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NPKDeptHeadTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MDCode",
                table: "TB_Networks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "KodeKareswil",
                table: "TB_Networks",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldUnicode: false,
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Karesidenan",
                table: "TB_Networks",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldUnicode: false,
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TB_Networks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "IdKaresidenanTSD",
                table: "TB_Networks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdKaresidenanHC3",
                table: "TB_Networks",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldUnicode: false,
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TB_Networks",
                type: "nvarchar(33)",
                maxLength: 33,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(33)",
                oldUnicode: false,
                oldMaxLength: 33,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DealerName",
                table: "TB_Networks",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldUnicode: false,
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "DLREmail",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TB_Networks",
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
                table: "TB_Networks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "TB_Networks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChannelDealer",
                table: "TB_Networks",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldUnicode: false,
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "AhmCode",
                table: "TB_Networks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "TB_Networks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNum",
                table: "TB_Networks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldMaxLength: 2147483647);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");
        }
    }
}
