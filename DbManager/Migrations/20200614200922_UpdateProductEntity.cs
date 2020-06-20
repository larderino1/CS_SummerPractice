using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class UpdateProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Category_CategoryId",
                table: "ShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShopItem");

            migrationBuilder.RenameTable(
                name: "ShopItem",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Product",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "ShopItem");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "ShopItem",
                newName: "IX_ShopItem_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ShopItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Category_CategoryId",
                table: "ShopItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
