using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class updateBooksColumn : Migration
    {
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE BookMaster SET Title = LEFT(Title,15) WHERE LEN(Title) > 15"
            );
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BookMaster",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "BookMaster",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 12f);

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 15f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BookMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "BookMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 10);

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 12);

            migrationBuilder.UpdateData(
                table: "BookMaster",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 15);
        }
    }
}
