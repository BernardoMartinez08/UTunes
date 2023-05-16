using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTunes.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class addingprices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Song",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 9.9900000000000002);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 7.9900000000000002);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 8.5600000000000005);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 5.9900000000000002);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Song",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Song",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 0);
        }
    }
}
