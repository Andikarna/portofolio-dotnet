using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshExpiredTime",
                table: "users_token",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users_token",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshExpiredTime",
                table: "users_token");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users_token");
        }
    }
}
