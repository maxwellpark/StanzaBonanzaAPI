using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanzaBonanza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TableAndColRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poem_Authors_Authors_AuthorId",
                table: "Poem_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Poem_Authors_Poems_PoemId",
                table: "Poem_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poem_Authors",
                table: "Poem_Authors");

            migrationBuilder.RenameTable(
                name: "Poem_Authors",
                newName: "Poems_Authors");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Poems",
                newName: "PoemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Poem_Authors_PoemId",
                table: "Poems_Authors",
                newName: "IX_Poems_Authors_PoemId");

            migrationBuilder.RenameIndex(
                name: "IX_Poem_Authors_AuthorId",
                table: "Poems_Authors",
                newName: "IX_Poems_Authors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poems_Authors",
                table: "Poems_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_Authors_Authors_AuthorId",
                table: "Poems_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_Authors_Poems_PoemId",
                table: "Poems_Authors",
                column: "PoemId",
                principalTable: "Poems",
                principalColumn: "PoemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poems_Authors_Authors_AuthorId",
                table: "Poems_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Poems_Authors_Poems_PoemId",
                table: "Poems_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poems_Authors",
                table: "Poems_Authors");

            migrationBuilder.RenameTable(
                name: "Poems_Authors",
                newName: "Poem_Authors");

            migrationBuilder.RenameColumn(
                name: "PoemId",
                table: "Poems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Poems_Authors_PoemId",
                table: "Poem_Authors",
                newName: "IX_Poem_Authors_PoemId");

            migrationBuilder.RenameIndex(
                name: "IX_Poems_Authors_AuthorId",
                table: "Poem_Authors",
                newName: "IX_Poem_Authors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poem_Authors",
                table: "Poem_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Poem_Authors_Authors_AuthorId",
                table: "Poem_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poem_Authors_Poems_PoemId",
                table: "Poem_Authors",
                column: "PoemId",
                principalTable: "Poems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
