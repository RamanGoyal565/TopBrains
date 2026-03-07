using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMaster", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BookMaster",
                columns: new[] { "Id", "Author", "Price", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 10, 1925, "The Great Gatsby" },
                    { 2, "Harper Lee", 12, 1960, "To Kill a Mockingbird" },
                    { 3, "George Orwell", 15, 1949, "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookMaster");
        }
    }
}
