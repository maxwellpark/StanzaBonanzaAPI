using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StanzaBonanza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Poems_Id",
                table: "Poems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Id",
                table: "Authors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Poems_Authors",
                columns: new[] { "Poem_AuthorId", "AuthorId", "PoemId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 4, 1, 3 },
                    { 5, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems_Authors",
                keyColumn: "Poem_AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Poems_Authors",
                keyColumn: "Poem_AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Poems_Authors",
                keyColumn: "Poem_AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Poems_Authors",
                keyColumn: "Poem_AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Poems_Authors",
                keyColumn: "Poem_AuthorId",
                keyValue: 5);
        }
    }
}
