using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanzaBonanza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Contributors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Poems",
                newName: "AuthorCreatorId");

            migrationBuilder.CreateTable(
                name: "AuthorPoem",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    PoemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPoem", x => new { x.AuthorsId, x.PoemsId });
                    table.ForeignKey(
                        name: "FK_AuthorPoem_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPoem_Poems_PoemsId",
                        column: x => x.PoemsId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPoem_PoemsId",
                table: "AuthorPoem",
                column: "PoemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorPoem");

            migrationBuilder.RenameColumn(
                name: "AuthorCreatorId",
                table: "Poems",
                newName: "AuthorId");
        }
    }
}
