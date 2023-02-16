using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoastcars.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationVehiclesToManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MakeId",
                table: "Vehicles",
                column: "MakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles",
                column: "MakeId",
                principalTable: "Manufacturers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MakeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "Vehicles");
        }
    }
}
