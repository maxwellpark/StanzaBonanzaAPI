using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanzaBonanza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PoemAuthorIdRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Poems_Authors",
                newName: "Poem_AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poem_AuthorId",
                table: "Poems_Authors",
                newName: "Id");
        }
    }
}
