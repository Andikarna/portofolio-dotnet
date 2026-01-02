using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.Migrations
{
  /// <inheritdoc />
  public partial class AddSkillEntity : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase(
          collation: "utf8mb4_general_ci",
          oldCollation: "utf8mb4_0900_ai_ci")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.AlterTable(
          name: "users_token")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_general_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterTable(
          name: "users")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_general_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterTable(
          name: "MasterRoles")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_general_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterTable(
          name: "Experiences")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_general_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "token",
          table: "users_token",
          type: "text",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "text",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "nama",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "ip_address",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "hostname",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RefreshToken",
          table: "users_token",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "password",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "name",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "email",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "MobilePhone",
          table: "users",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RoleName",
          table: "MasterRoles",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RoleDescription",
          table: "MasterRoles",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Title",
          table: "Experiences",
          type: "longtext",
          nullable: false,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Status",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Skills",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Description",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Company",
          table: "Experiences",
          type: "longtext",
          nullable: false,
          collation: "utf8mb4_general_ci",
          oldClrType: typeof(string),
          oldType: "longtext")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

      // migrationBuilder.CreateTable(
      //     name: "skills",
      //     columns: table => new
      //     {
      //         id = table.Column<int>(type: "int", nullable: false)
      //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
      //         name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         level = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         icon_url = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         is_featured = table.Column<bool>(type: "tinyint(1)", nullable: false),
      //         created_date = table.Column<DateTime>(type: "datetime", nullable: true),
      //         update_date = table.Column<DateTime>(type: "datetime", nullable: true)
      //     },
      //     constraints: table =>
      //     {
      //         table.PrimaryKey("PK_skills", x => x.id);
      //     })
      //     .Annotation("MySql:CharSet", "utf8mb4")
      //     .Annotation("Relational:Collation", "utf8mb4_general_ci");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "skills");

      migrationBuilder.AlterDatabase(
          collation: "utf8mb4_0900_ai_ci",
          oldCollation: "utf8mb4_general_ci")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.AlterTable(
          name: "users_token")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterTable(
          name: "users")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterTable(
          name: "MasterRoles")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterTable(
          name: "Experiences")
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "token",
          table: "users_token",
          type: "text",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "text",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "nama",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "ip_address",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "hostname",
          table: "users_token",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RefreshToken",
          table: "users_token",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "password",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "name",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "email",
          table: "users",
          type: "varchar(100)",
          maxLength: 100,
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "varchar(100)",
          oldMaxLength: 100,
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "MobilePhone",
          table: "users",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RoleName",
          table: "MasterRoles",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "RoleDescription",
          table: "MasterRoles",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Title",
          table: "Experiences",
          type: "longtext",
          nullable: false,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Status",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Skills",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Description",
          table: "Experiences",
          type: "longtext",
          nullable: true,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext",
          oldNullable: true)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

      migrationBuilder.AlterColumn<string>(
          name: "Company",
          table: "Experiences",
          type: "longtext",
          nullable: false,
          collation: "utf8mb4_0900_ai_ci",
          oldClrType: typeof(string),
          oldType: "longtext")
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
    }
  }
}
