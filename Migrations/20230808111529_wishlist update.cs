using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_cart.Migrations
{
    /// <inheritdoc />
    public partial class wishlistupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Products_ProductId",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            migrationBuilder.RenameTable(
                name: "WishList",
                newName: "wishlist");

            migrationBuilder.RenameIndex(
                name: "IX_WishList_UserId",
                table: "wishlist",
                newName: "IX_wishlist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WishList_ProductId",
                table: "wishlist",
                newName: "IX_wishlist_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wishlist",
                table: "wishlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_wishlist_Products_ProductId",
                table: "wishlist",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlist_Users_UserId",
                table: "wishlist",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wishlist_Products_ProductId",
                table: "wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlist_Users_UserId",
                table: "wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wishlist",
                table: "wishlist");

            migrationBuilder.RenameTable(
                name: "wishlist",
                newName: "WishList");

            migrationBuilder.RenameIndex(
                name: "IX_wishlist_UserId",
                table: "WishList",
                newName: "IX_WishList_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_wishlist_ProductId",
                table: "WishList",
                newName: "IX_WishList_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Products_ProductId",
                table: "WishList",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Users_UserId",
                table: "WishList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
