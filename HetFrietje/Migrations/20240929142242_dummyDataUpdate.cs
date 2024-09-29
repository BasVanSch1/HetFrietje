using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HetFrietje.Migrations
{
    /// <inheritdoc />
    public partial class dummyDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Stock" },
                values: new object[] { "Klasieke cheeseburger zoals iedereen hem kent.", "Cheeseburger", 32 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "De burger.. met spek. maar dan in schotel variant?", "Spekburger schotel" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Name", "SalesPrice" },
                values: new object[] { "Onze beste friet, gemaakt van 5% aardappelen en 95% zout.", "Friet", 12m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price", "SalesPrice", "Stock", "Tax" },
                values: new object[] { 4, "De geweldige super burger, met 0 % natuurlijk vlees.", "Superburger", 12.65m, 12m, 18, 9.00m });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                values: new object[,]
                {
                    { 2, 4 },
                    { 7, 4 },
                    { 8, 4 }
                });

            migrationBuilder.InsertData(
                table: "OptionProduct",
                columns: new[] { "OptionsOptionId", "ProductsProductId" },
                values: new object[] { 1, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "CategoryProduct",
                keyColumns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "OptionProduct",
                keyColumns: new[] { "OptionsOptionId", "ProductsProductId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesCategoryId", "ProductsProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 4, 2 },
                    { 7, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Stock" },
                values: new object[] { "Voorbeeld product 1", "Product 1", 152 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Voorbeeld product 2", "Product 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Name", "SalesPrice" },
                values: new object[] { "Voorbeeld product 3", "Product 3", null });
        }
    }
}
