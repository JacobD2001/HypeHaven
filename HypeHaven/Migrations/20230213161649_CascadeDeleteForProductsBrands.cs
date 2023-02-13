using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HypeHaven.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteForProductsBrands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    brandid = table.Column<int>(name: "brand_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phonenumber = table.Column<string>(name: "phone_number", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    instagram = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    facebook = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    pinterest = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    tiktok = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    video = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    dateadded = table.Column<DateTime>(name: "date_added", type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    categoryid = table.Column<int>(name: "category_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__brands__5E5A8E270E6B71A1", x => x.brandid);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryid = table.Column<int>(name: "category_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    parentid = table.Column<int>(name: "parent_id", type: "int", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__D54EE9B45A129DE4", x => x.categoryid);
                    table.ForeignKey(
                        name: "FK__categorie__paren__4CA06362",
                        column: x => x.parentid,
                        principalTable: "categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    paymentid = table.Column<int>(name: "payment_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    paymentmethod = table.Column<string>(name: "payment_method", type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    paidamount = table.Column<decimal>(name: "paid_amount", type: "decimal(10,2)", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payments__ED1FC9EAB83116EA", x => x.paymentid);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    size = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    color = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    material = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    dateadded = table.Column<DateTime>(name: "date_added", type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    brandid = table.Column<int>(name: "brand_id", type: "int", nullable: false),
                    categoryid = table.Column<int>(name: "category_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__47027DF5C7801DC4", x => x.productid);
                    table.ForeignKey(
                        name: "FK__products__brand___6383C8BA",
                        column: x => x.brandid,
                        principalTable: "brands",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderid = table.Column<int>(name: "order_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    orderdate = table.Column<DateTime>(name: "order_date", type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    shippingaddress = table.Column<string>(name: "shipping_address", type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    orderstatus = table.Column<string>(name: "order_status", type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    trackingnumber = table.Column<string>(name: "tracking_number", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    deliverydate = table.Column<DateTime>(name: "delivery_date", type: "date", nullable: true),
                    shippingmethod = table.Column<string>(name: "shipping_method", type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    promocode = table.Column<string>(name: "promo_code", type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    paymentsid = table.Column<int>(name: "payments_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__46596229B0138C3B", x => x.orderid);
                    table.ForeignKey(
                        name: "FK__orders__payments__6B24EA82",
                        column: x => x.paymentsid,
                        principalTable: "payments",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "FK__orders__product___6A30C649",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewid = table.Column<int>(name: "review_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brandid = table.Column<int>(name: "brand_id", type: "int", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    rating = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(name: "created_at", type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__60883D90800C4BBA", x => x.reviewid);
                    table.ForeignKey(
                        name: "FK__reviews__brand_i__72C60C4A",
                        column: x => x.brandid,
                        principalTable: "brands",
                        principalColumn: "brand_id");
                    table.ForeignKey(
                        name: "FK__reviews__product__73BA3083",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    orderitemid = table.Column<int>(name: "order_item_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderid = table.Column<int>(name: "order_id", type: "int", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_it__3764B6BC0338597A", x => x.orderitemid);
                    table.ForeignKey(
                        name: "FK__order_ite__order__6E01572D",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__order_ite__produ__6EF57B66",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_payments_id",
                table: "orders",
                column: "payments_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_product_id",
                table: "orders",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_brand_id",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_brand_id",
                table: "reviews",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_product_id",
                table: "reviews",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
