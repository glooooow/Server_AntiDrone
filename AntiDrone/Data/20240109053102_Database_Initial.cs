using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AntiDrone.Data
{
    /// <inheritdoc />
    public partial class Database_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Whitelist",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    affiliation = table.Column<string>(type: "longtext", nullable: false),
                    operator_name = table.Column<string>(type: "longtext", nullable: false),
                    contact = table.Column<string>(type: "longtext", nullable: false),
                    drone_type = table.Column<string>(type: "longtext", nullable: false),
                    drone_model = table.Column<string>(type: "longtext", nullable: false),
                    drone_id = table.Column<string>(type: "longtext", nullable: false),
                    memo = table.Column<string>(type: "longtext", nullable: false),
                    approval_state = table.Column<string>(type: "tinyint", nullable: false),
                    approval_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    approval_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    now_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whitelist", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Whitelist");
        }
    }
}
