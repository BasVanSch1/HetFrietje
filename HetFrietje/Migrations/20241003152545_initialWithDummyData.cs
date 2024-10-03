using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HetFrietje.Migrations
{
    /// <inheritdoc />
    public partial class initialWithDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionLevel = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesCategoryId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionProduct",
                columns: table => new
                {
                    OptionsOptionId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionProduct", x => new { x.OptionsOptionId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_OptionProduct_Options_OptionsOptionId",
                        column: x => x.OptionsOptionId,
                        principalTable: "Options",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionProduct_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SubtotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Schotels" },
                    { 2, "Hamburgers" },
                    { 3, "Frieten" },
                    { 4, "Snacks" },
                    { 5, "Vega" },
                    { 6, "Frisdrank" },
                    { 7, "Aanbiedingen" },
                    { 8, "Overige" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionId", "Name", "Values" },
                values: new object[,]
                {
                    { 1, "Maat", "[\"Small\",\"Medium\",\"Large\",\"Extra Large\"]" },
                    { 2, "Saus", "[\"Knoflook\",\"Sambal\",\"Curry\",\"Mosterd\",\"Mayonaise\"]" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price", "SalesPrice", "Stock", "Tax" },
                values: new object[,]
                {
                    { 1, "Klasieke cheeseburger zoals iedereen hem kent.", "Cheeseburger", 1.50m, null, 32, 9.00m },
                    { 2, "De burger.. met spek. maar dan in schotel variant?", "Spekburger schotel", 7.45m, null, 76, 9.00m },
                    { 3, "Onze beste friet, gemaakt van 5% aardappelen en 95% zout.", "Friet", 54.65m, 12m, 26, 9.00m },
                    { 4, "De geweldige super burger, met 0 % natuurlijk vlees.", "Superburger", 12.65m, 12m, 18, 9.00m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Name", "PermissionLevel", "password" },
                values: new object[] { "Klant", "Klant 1", 1, null });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 3 },
                    { 7, 3 },
                    { 7, 4 },
                    { 8, 4 }
                });

            migrationBuilder.InsertData(
                table: "OptionProduct",
                columns: new[] { "OptionsOptionId", "ProductsProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Status", "SubtotalPrice", "TotalPrice", "Username" },
                values: new object[] { 1, 2, 123m, 123m, "Klant" });

            migrationBuilder.InsertData(
                table: "ProductOrders",
                columns: new[] { "OrderId", "ProductId", "ProductCount" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 1, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsProductId",
                table: "CategoryProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionProduct_ProductsProductId",
                table: "OptionProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Username",
                table: "Orders",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "OptionProduct");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
