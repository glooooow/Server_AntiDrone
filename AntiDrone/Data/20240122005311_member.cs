using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AntiDrone.Data
{
    /// <inheritdoc />
    public partial class member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "now_date",
                table: "Whitelist",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "approval_start_date",
                table: "Whitelist",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "approval_end_date",
                table: "Whitelist",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    authority = table.Column<long>(type: "bigint", nullable: false),
                    permission_state = table.Column<long>(type: "bigint", nullable: false),
                    member_id = table.Column<string>(type: "longtext", nullable: false),
                    member_pw = table.Column<string>(type: "longtext", nullable: false),
                    member_name = table.Column<string>(type: "longtext", nullable: false),
                    member_email = table.Column<string>(type: "longtext", nullable: false),
                    member_contact = table.Column<string>(type: "longtext", nullable: false),
                    join_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    latest_access_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "now_date",
                table: "Whitelist",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "approval_start_date",
                table: "Whitelist",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "approval_end_date",
                table: "Whitelist",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
