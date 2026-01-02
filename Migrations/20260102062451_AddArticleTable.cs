using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.Migrations
{
  /// <inheritdoc />
  public partial class AddArticleTable : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "articles",
          columns: table => new
          {
            id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                  .Annotation("MySql:CharSet", "utf8mb4"),
            content = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                  .Annotation("MySql:CharSet", "utf8mb4"),
            image_base64 = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
                  .Annotation("MySql:CharSet", "utf8mb4"),
            publication_date = table.Column<DateTime>(type: "datetime", nullable: false),
            tags = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
                  .Annotation("MySql:CharSet", "utf8mb4"),
            created_date = table.Column<DateTime>(type: "datetime", nullable: false),
            update_date = table.Column<DateTime>(type: "datetime", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_articles", x => x.id);
          })
          .Annotation("MySql:CharSet", "utf8mb4")
          .Annotation("Relational:Collation", "utf8mb4_general_ci");

      // migrationBuilder.CreateTable(
      //     name: "projects",
      //     columns: table => new
      //     {
      //         id = table.Column<int>(type: "int", nullable: false)
      //             .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
      //         title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         cover_image_url = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         repository_url = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         demo_url = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         start_date = table.Column<DateTime>(type: "datetime", nullable: true),
      //         technologies = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         summary = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         description = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_general_ci")
      //             .Annotation("MySql:CharSet", "utf8mb4"),
      //         is_featured = table.Column<bool>(type: "tinyint(1)", nullable: false),
      //         created_date = table.Column<DateTime>(type: "datetime", nullable: true),
      //         update_date = table.Column<DateTime>(type: "datetime", nullable: true)
      //     },
      //     constraints: table =>
      //     {
      //         table.PrimaryKey("PK_projects", x => x.id);
      //     })
      //     .Annotation("MySql:CharSet", "utf8mb4")
      //     .Annotation("Relational:Collation", "utf8mb4_general_ci");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "articles");

      migrationBuilder.DropTable(
          name: "projects");
    }
  }
}
