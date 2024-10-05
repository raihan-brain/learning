using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace learning.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillingDate",
                table: "employees",
                newName: "BillingRate");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressLine2", "AddressLine3", "Addresszline1", "City", "Country", "PostalCode", "StateProvince" },
                values: new object[,]
                {
                    { 1, null, null, "123 Main St", "Anytown", null, "12345", "NY" },
                    { 2, null, null, "1234 Main St", "Anytown", null, "1345", "CA" }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "BillingRate", "Email", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 100.0, "tee@yopmail.com", "https://via.placeholder.com/150", "John Doe" },
                    { 2, 100.0, "bee@yopmail.com", "https://via.placeholder.com/150", "Bret Lw" },
                    { 3, 100.0, "twit@yopmail.com", "https://via.placeholder.com/150", "twitter" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "AddressId", "CompanyName", "Contact", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "ABC Corp", "John Doe", "123-456-7890" },
                    { 2, 2, "ACI", "John YEEE", "123-456-7890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "BillingRate",
                table: "employees",
                newName: "BillingDate");
        }
    }
}
