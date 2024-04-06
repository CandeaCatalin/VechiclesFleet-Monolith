using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesFleet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FixDate",
                table: "VehiclesErrors",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "VehiclesAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MinimumSpeed = table.Column<int>(type: "int", nullable: false),
                    MaximumSpeed = table.Column<int>(type: "int", nullable: false),
                    AverageSpeed = table.Column<double>(type: "float", nullable: false),
                    FuelConsumption = table.Column<double>(type: "float", nullable: false),
                    HasTierPressureAnomaly = table.Column<bool>(type: "bit", nullable: false),
                    TotalKilometersPassed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclesAnalysis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehiclesAnalysis_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesAnalysis_UserId",
                table: "VehiclesAnalysis",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesAnalysis_VehicleId",
                table: "VehiclesAnalysis",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclesAnalysis");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FixDate",
                table: "VehiclesErrors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
