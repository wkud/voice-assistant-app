using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShopsAndShoppingItemsAndShopProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shopping_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shopping_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shops",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    website_url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shops", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shop_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_product_name = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    image_url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    amount_per_item_value = table.Column<decimal>(type: "numeric", nullable: true),
                    amount_per_item_unit = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    shop_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shopping_item_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shop_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_shop_products_shopping_items_shopping_item_id",
                        column: x => x.shopping_item_id,
                        principalTable: "shopping_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_shop_products_shops_shop_id",
                        column: x => x.shop_id,
                        principalTable: "shops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_shop_products_created_at",
                table: "shop_products",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_shop_products_shop_id",
                table: "shop_products",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_products_shopping_item_id",
                table: "shop_products",
                column: "shopping_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shopping_items_created_at",
                table: "shopping_items",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_shopping_items_name",
                table: "shopping_items",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_shops_created_at",
                table: "shops",
                column: "created_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop_products");

            migrationBuilder.DropTable(
                name: "shopping_items");

            migrationBuilder.DropTable(
                name: "shops");
        }
    }
}
