using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoastcars.web.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedMakeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "MakeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles",
                column: "MakeId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "MakeId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_MakeId",
                table: "Vehicles",
                column: "MakeId",
                principalTable: "Manufacturers",
                principalColumn: "Id");
        }
    }
}
