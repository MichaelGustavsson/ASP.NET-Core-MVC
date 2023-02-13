using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoastcars.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationVehicleToFuelType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelTypeId",
                table: "Vehicles",
                column: "FuelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_FuelTypes_FuelTypeId",
                table: "Vehicles",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_FuelTypes_FuelTypeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FuelTypeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelTypeId",
                table: "Vehicles");
        }
    }
}
