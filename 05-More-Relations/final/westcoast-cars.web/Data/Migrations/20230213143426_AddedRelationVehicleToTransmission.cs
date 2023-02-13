using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoastcars.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationVehicleToTransmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransmissionsTypeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TransmissionsTypeId",
                table: "Vehicles",
                column: "TransmissionsTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TransmissionsTypes_TransmissionsTypeId",
                table: "Vehicles",
                column: "TransmissionsTypeId",
                principalTable: "TransmissionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TransmissionsTypes_TransmissionsTypeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TransmissionsTypeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TransmissionsTypeId",
                table: "Vehicles");
        }
    }
}
