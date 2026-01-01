using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTP.Migrations
{
  /// <inheritdoc />
  public partial class FixSkillsTable : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS `skills` (
                    `id` int NOT NULL AUTO_INCREMENT,
                    `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
                    `category` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
                    `level` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
                    `icon_url` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
                    `is_featured` tinyint(1) NOT NULL,
                    `created_date` datetime NULL,
                    `update_date` datetime NULL,
                    PRIMARY KEY (`id`)
                ) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_general_ci;
            ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
  }
}
