using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AntiDrone.Data
{
    /// <inheritdoc />
    public partial class HeaderAndScannerDetections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryHeader",
                columns: table => new
                {
                    meta_master_key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    meta_data_code = table.Column<string>(type: "longtext", nullable: false),
                    meta_data_name = table.Column<string>(type: "longtext", nullable: false),
                    meta_data_type = table.Column<string>(type: "longtext", nullable: false),
                    meta_data_id = table.Column<string>(type: "longtext", nullable: false),
                    meta_writer = table.Column<string>(type: "longtext", nullable: true),
                    meta_memo = table.Column<string>(type: "longtext", nullable: true),
                    meta_near_lat = table.Column<double>(type: "double", nullable: false),
                    meta_near_lon = table.Column<double>(type: "double", nullable: false),
                    meta_near_operator_lat = table.Column<double>(type: "double", nullable: true),
                    meta_near_operator_lon = table.Column<double>(type: "double", nullable: true),
                    meta_mfr = table.Column<string>(type: "longtext", nullable: true),
                    det_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    det_start_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    det_end_time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryHeader", x => x.meta_master_key);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ScannerDetections",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    code = table.Column<string>(type: "longtext", nullable: false),
                    port = table.Column<string>(type: "longtext", nullable: false),
                    sc_det_id = table.Column<string>(type: "longtext", nullable: true),
                    model = table.Column<string>(type: "longtext", nullable: true),
                    latitude = table.Column<double>(type: "double", nullable: false),
                    longitude = table.Column<double>(type: "double", nullable: false),
                    altitude = table.Column<double>(type: "double", nullable: true),
                    elevation = table.Column<double>(type: "double", nullable: true),
                    velocity = table.Column<double>(type: "double", nullable: true),
                    course = table.Column<double>(type: "double", nullable: true),
                    angle = table.Column<double>(type: "double", nullable: true),
                    range = table.Column<double>(type: "double", nullable: true),
                    frequency = table.Column<double>(type: "double", nullable: true),
                    operator_lat = table.Column<double>(type: "double", nullable: true),
                    operator_lon = table.Column<double>(type: "double", nullable: true),
                    mfr = table.Column<string>(type: "longtext", nullable: true),
                    protocol = table.Column<string>(type: "longtext", nullable: true),
                    mac_address1 = table.Column<string>(type: "longtext", nullable: true),
                    mac_address2 = table.Column<string>(type: "longtext", nullable: true),
                    det_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    det_time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannerDetections", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryHeader");

            migrationBuilder.DropTable(
                name: "ScannerDetections");
        }
    }
}
