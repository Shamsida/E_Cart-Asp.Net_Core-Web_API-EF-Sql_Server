using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_cart.Migrations
{
    /// <inheritdoc />
    public partial class updateorderdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickupName",
                table: "Orders",
                newName: "PickupAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PickupAddress",
                table: "Orders",
                newName: "PickupName");
        }
    }
}
